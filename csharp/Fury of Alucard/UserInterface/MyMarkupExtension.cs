using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace Fury_of_Alucard.UserInterface
{
	public class MyMarkupExtension<T> : MarkupExtension
		where T : class, new()
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new T();
		}
	}
}
