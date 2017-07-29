using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CSharp;
using System.Reflection.Emit;

namespace Fury_of_Alucard.Common
{
	public static class TypeFactory
	{
		public static Type AutoNotifier(Type t)
		{
			if (NothingToDoWith(t))
				return t;
			Prerequisites.ThrowIfNotSatisfiedBy(t);
			var influences = PropertyDependencyAnalyzer.GetPropertyInfluences(t);
			return ProxyGen.GetFor(t, influences);
		}
		public static Type AutoNotifier<T>()
		{
			return AutoNotifier(typeof(T));
		}
		public static bool NothingToDoWith(Type t)
		{
			if (!t.IsSealed && t.IsPublic)
			{
				return !t.GetProperties().Any(q => q.IsVirtual() && q.CanRead && q.CanWrite);
			}
			return true;
		}
		public static bool IsVirtual(this PropertyInfo pi)
		{
			return (pi.CanRead == false || pi.GetGetMethod().IsVirtual) && (pi.CanWrite == false || pi.GetSetMethod().IsVirtual);
		}

		internal static T1 CreateAutoNotifierInstance<T1>(params object[] args)
			where T1 : class
		{
			return Activator.CreateInstance(TypeFactory.AutoNotifier(typeof(T1)), args) as T1;
		}
	}
	class Prerequisites
	{
		public static void ThrowIfNotSatisfiedBy(Type t)
		{
			if (t.GetInterfaces().Any(I => I == typeof(INotifyPropertyChanged)))
			{
				var mi = t.GetMethod(ProxyGen.PropertyChangedFunctionName, BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);


				if (mi == null)
					throw new Exception(t.Name + " does not implement a function named " + ProxyGen.PropertyChangedFunctionName + "(string) to raise the property changed event.");
				if (mi.IsPrivate)
					throw new Exception(t.Name + "." + ProxyGen.PropertyChangedFunctionName + " must be public or protected.");
			}
		}
	}
	static class ProxyGen
	{
		static object lockobject = new object();
		static Dictionary<Type, Type> proxies = new Dictionary<Type, Type>();
		internal const string PropertyChangedFunctionName = "OnPropertyChanged";
		internal const string PropertyChangedEventName = "PropertyChanged";
		private const string PropertyNameParameterName = "propertyName";
		private const string EventHandlerName = "handler";

		public static Type GetFor(Type t, Dictionary<string, List<string>> influences)
		{
			if (HasFor(t))
				return For(t);
			Store(CreateFor(t, influences), t);
			return For(t);
		}

		private static void Store(Type type, Type key)
		{
			lock (lockobject)
			{
				proxies[key] = type;
			}
		}

		private static Type CreateFor(Type t, Dictionary<string, List<string>> influences)
		{
			var provider = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters();
			cp.GenerateInMemory = true;
			CodeCompileUnit cu = new CodeCompileUnit();
			AddAllAssemblyAsReference(cu);
			cu.Namespaces.Add(CreateNamespace(t, influences));
#if DEBUG
			StringWriter sw = new StringWriter();
			provider.GenerateCodeFromCompileUnit(cu, sw, new CodeGeneratorOptions() { BracingStyle = "C" });
			//Console.WriteLine(sw.GetStringBuilder());
#endif
			CompilerResults cr = provider.CompileAssemblyFromDom(cp, cu);
			if (cr.Errors.Count > 0)
			{
				ThrowErrors(cr.Errors);
			}
			return cr.CompiledAssembly.GetTypes()[0];
		}

		private static void AddAllAssemblyAsReference(CodeCompileUnit cu)
		{
			foreach (Assembly v in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					if(!v.IsDynamic)
						cu.ReferencedAssemblies.Add(v.Location);
				}
				catch (NotSupportedException)
				{
					// happens for dynamic assemblies, ignore it
				}
			}
		}

		private static void ThrowErrors(CompilerErrorCollection compilerErrorCollection)
		{
			StringBuilder sb = new StringBuilder();
			foreach (CompilerError e in compilerErrorCollection)
			{
				sb.AppendLine(e.ErrorText);
			}
			throw new Exception("Compiler errors:\n" + sb.ToString());
		}

		private static CodeNamespace CreateNamespace(Type t, Dictionary<string, List<string>> influences)
		{
			var nsp = new CodeNamespace("__autonotifypropertychanged");
			var decl = new CodeTypeDeclaration();
			decl.Name = GetNameForDerivedClass(t);
			decl.TypeAttributes = TypeAttributes.NotPublic;
			decl.Attributes = MemberAttributes.Private;
			decl.BaseTypes.Add(t);
			if (!t.GetInterfaces().Any(I => I == typeof(INotifyPropertyChanged)))
			{
				ImplementINotifyPropertyChanged(decl);
			}
			foreach (var ci in t.GetConstructors())
			{
				if (ci.IsPublic && ci.GetParameters().Length > 0)
				{
					AddDerivedConstructor(decl, ci);
				}
			}
			foreach (PropertyInfo pi in t.GetProperties().Where(p => p.IsVirtual()))
			{
				// make sense only to override properties having setter and getter
				if (pi.CanWrite && pi.CanRead)
				{
					List<string> additional = new List<string>();
					if (influences.ContainsKey(pi.Name))
						additional = influences[pi.Name];
					decl.Members.Add(CreatePropertyOverride(pi, additional));
				}
			}
			nsp.Types.Add(decl);
			return nsp;
		}

		private static void ImplementINotifyPropertyChanged(CodeTypeDeclaration decl)
		{
			decl.BaseTypes.Add(typeof(INotifyPropertyChanged));
			decl.Members.Add(new CodeMemberEvent()
							{
								Name = PropertyChangedEventName
							 ,
								Attributes = MemberAttributes.Public
							 ,
								Type = new CodeTypeReference(typeof(PropertyChangedEventHandler))
							});
			var notify = new CodeMemberMethod()
							{
								Name = PropertyChangedFunctionName
								,
								Attributes = MemberAttributes.Family

							};
			decl.Members.Add(notify);



			notify.Parameters.Add(new CodeParameterDeclarationExpression() { Name = PropertyNameParameterName, Type = new CodeTypeReference(typeof(string)) });

			notify.Statements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(PropertyChangedEventHandler)), EventHandlerName));
			notify.Statements.Add(new CodeAssignStatement() { Left = new CodeVariableReferenceExpression(EventHandlerName), Right = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), PropertyChangedEventName) });

			var condition = new CodeConditionStatement()
			{
				Condition = new CodeBinaryOperatorExpression()
			  {
				  Left = new CodePrimitiveExpression(null)
				  ,
				  Right = new CodeVariableReferenceExpression(EventHandlerName)
				  ,
				  Operator = CodeBinaryOperatorType.IdentityInequality
			  }
			};
			var eventArgs = new CodeObjectCreateExpression() { CreateType = new CodeTypeReference(typeof(PropertyChangedEventArgs)) };
			eventArgs.Parameters.Add(new CodeVariableReferenceExpression(PropertyNameParameterName));

			var invoke = new CodeMethodInvokeExpression(null, EventHandlerName);
			invoke.Parameters.Add(new CodeThisReferenceExpression());
			invoke.Parameters.Add(eventArgs);
			condition.TrueStatements.Add(
					invoke
				);

			notify.Statements.Add(condition);
		}

		private static void AddDerivedConstructor(CodeTypeDeclaration decl, ConstructorInfo ci)
		{
			CodeConstructor cc = new CodeConstructor();
			cc.Attributes = MemberAttributes.Public;
			foreach (var pi in ci.GetParameters())
			{
				cc.Parameters.Add(new CodeParameterDeclarationExpression() { Name = pi.Name, Type = new CodeTypeReference(pi.ParameterType), Direction = ToDirection(pi) });
				cc.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(pi.Name));
			}

			decl.Members.Add(cc);
		}

		private static FieldDirection ToDirection(ParameterInfo pi)
		{
			if (pi.IsIn)
				return FieldDirection.In;
			if (pi.IsOut)
				return FieldDirection.Out;
			if (pi.ParameterType.IsByRef)
				return FieldDirection.Ref;
			return FieldDirection.In;
		}



		private static CodeTypeMember CreatePropertyOverride(PropertyInfo pi, List<string> additionals)
		{
			CodeMemberProperty mp = new CodeMemberProperty();
			mp.Name = pi.Name;
			mp.Attributes = MemberAttributes.Override | MemberAttributes.Public;
			mp.Type = new CodeTypeReference(pi.PropertyType);
			mp.HasGet = mp.HasSet = true;

			mp.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression() { PropertyName = pi.Name, TargetObject = new CodeBaseReferenceExpression() }));

			var conditionForNotify = new CodeMethodInvokeExpression()
			{
				Method = new CodeMethodReferenceExpression(
					new CodePropertyReferenceExpression()
				{ PropertyName = pi.Name,
					TargetObject = new CodeBaseReferenceExpression()
				},
				"Equals")
			};
			conditionForNotify.Parameters.Add(new CodePropertySetValueReferenceExpression());
			var setCondition = new CodeConditionStatement()
			{
				Condition = new CodeBinaryOperatorExpression()
				{
					Left = new CodePrimitiveExpression(false),
					Right = conditionForNotify,
					Operator = CodeBinaryOperatorType.IdentityEquality
				}
			};
			var assignStatement = new CodeAssignStatement()
			{
				Left = new CodePropertyReferenceExpression()
				{
					PropertyName = pi.Name,
					TargetObject = new CodeBaseReferenceExpression()
				},
				Right = new CodePropertySetValueReferenceExpression()
			};
			setCondition.TrueStatements.Add(
				assignStatement
			);


			var invokeMethod = new CodeMethodInvokeExpression() { Method = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), PropertyChangedFunctionName) };
			invokeMethod.Parameters.Add(new CodePrimitiveExpression(pi.Name));
			setCondition.TrueStatements.Add(invokeMethod);

			foreach (var additional in additionals)
			{
				invokeMethod = new CodeMethodInvokeExpression() { Method = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), PropertyChangedFunctionName) };
				invokeMethod.Parameters.Add(new CodePrimitiveExpression(additional));
				setCondition.TrueStatements.Add(invokeMethod);
			}

			mp.SetStatements.Add(assignStatement);
			mp.SetStatements.Add(invokeMethod);
			return mp;
		}



		private static string GetNameForDerivedClass(Type t)
		{
			return string.Concat("__autonotify", t.Name);
		}

		private static bool HasFor(Type t)
		{
			lock (lockobject)
			{
				return proxies.ContainsKey(t);
			}
		}

		private static Type For(Type t)
		{
			lock (lockobject)
			{
				return proxies[t];
			}
		}
	}
	class PropertyDependencyAnalyzer : IEnumerable<MethodBase>
	{
		Byte[] bytes;
		Int32 pos;
		MethodBase method;
		static OpCode[] smallOpCodes = new OpCode[0x100];
		static OpCode[] largeOpCodes = new OpCode[0x100];
		static PropertyDependencyAnalyzer()
		{
			foreach (FieldInfo fi in typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				OpCode opCode = (OpCode)fi.GetValue(null);
				UInt16 value = (UInt16)opCode.Value;
				if (value < 0x100)
					smallOpCodes[value] = opCode;
				else if ((value & 0xff00) == 0xfe00)
					largeOpCodes[value & 0xff] = opCode;
			}
		}


		public static Dictionary<string, List<string>> GetPropertyInfluences(Type t)
		{
			Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
			try
			{
				foreach (PropertyInfo pi in t.GetProperties().Where(q => q.IsVirtual()))
				{
					var analyzer = new PropertyDependencyAnalyzer(pi.GetGetMethod());
					foreach (var methodBase in analyzer)
					{
						if (methodBase.DeclaringType == t && methodBase.IsSpecialName && methodBase.Name.StartsWith("get_"))
						{
							// property dependency found
							StoreFound(map, pi.Name, methodBase.Name.Substring(4));
						}
					}
				}
			}
			catch
			{
				//ignore IL exception
			}
			return map;
		}

		private static void StoreFound(Dictionary<string, List<string>> map, string influenced, string by)
		{
			if (!map.ContainsKey(by))
			{
				map[by] = new List<string>();
			}
			if (!map[by].Contains(influenced))
				map[by].Add(influenced);
		}
		Module module;
		private PropertyDependencyAnalyzer(MethodBase enclosingMethod)
		{
			this.method = enclosingMethod;
			module = method.DeclaringType.Assembly.GetModules().Where(m => m.GetTypes().Any(t => t == method.DeclaringType)).FirstOrDefault();
			MethodBody methodBody = method.GetMethodBody();
			this.bytes = (methodBody == null) ? new Byte[0] : methodBody.GetILAsByteArray();
			this.pos = 0;
		}

		public IEnumerator<MethodBase> GetEnumerator()
		{
			while (pos < bytes.Length)
			{
				var x = Next();
				if (null == x)
				{
					pos = 0;
					yield break;
				}
				yield return x;
			}
			pos = 0;
			yield break;
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

		MethodBase Next()
		{
			while (pos < bytes.Length)
			{
				Int32 offset = pos;
				OpCode opCode = OpCodes.Nop;
				Int32 token = 0;

				// read first 1 or 2 bytes as opCode
				Byte code = ReadByte();
				if (code != 0xFE)
					opCode = smallOpCodes[code];
				else
				{
					code = ReadByte();
					opCode = largeOpCodes[code];
				}

				switch (opCode.OperandType)
				{
					case OperandType.InlineNone:
						continue;

					case OperandType.ShortInlineBrTarget:
						SByte shortDelta = ReadSByte();
						continue;

					case OperandType.InlineBrTarget: Int32 delta = ReadInt32(); continue;
					case OperandType.ShortInlineI: Byte int8 = ReadByte(); continue;
					case OperandType.InlineI: Int32 int32 = ReadInt32(); continue;
					case OperandType.InlineI8: Int64 int64 = ReadInt64(); continue;
					case OperandType.ShortInlineR: Single float32 = ReadSingle(); continue;
					case OperandType.InlineR: Double float64 = ReadDouble(); continue;
					case OperandType.ShortInlineVar: Byte index8 = ReadByte(); continue;
					case OperandType.InlineVar: UInt16 index16 = ReadUInt16(); continue;
					case OperandType.InlineString: token = ReadInt32(); continue;
					case OperandType.InlineSig: token = ReadInt32(); continue;
					case OperandType.InlineField: token = ReadInt32(); continue;
					case OperandType.InlineType: token = ReadInt32(); continue;
					case OperandType.InlineTok: token = ReadInt32(); continue;

					case OperandType.InlineMethod:
						token = ReadInt32();
						return module.ResolveMethod(token);

					case OperandType.InlineSwitch:
						Int32 cases = ReadInt32();
						Int32[] deltas = new Int32[cases];
						for (Int32 i = 0; i < cases; i++) deltas[i] = ReadInt32();
						continue;

					default:
						throw new BadImageFormatException("unexpected OperandType " + opCode.OperandType);
				}
			}
			return null;
		}



		Byte ReadByte() { return (Byte)bytes[pos++]; }
		SByte ReadSByte() { return (SByte)ReadByte(); }

		UInt16 ReadUInt16() { pos += 2; return BitConverter.ToUInt16(bytes, pos - 2); }
		UInt32 ReadUInt32() { pos += 4; return BitConverter.ToUInt32(bytes, pos - 4); }
		UInt64 ReadUInt64() { pos += 8; return BitConverter.ToUInt64(bytes, pos - 8); }

		Int32 ReadInt32() { pos += 4; return BitConverter.ToInt32(bytes, pos - 4); }
		Int64 ReadInt64() { pos += 8; return BitConverter.ToInt64(bytes, pos - 8); }

		Single ReadSingle() { pos += 4; return BitConverter.ToSingle(bytes, pos - 4); }
		Double ReadDouble() { pos += 8; return BitConverter.ToDouble(bytes, pos - 8); }
	}
}
