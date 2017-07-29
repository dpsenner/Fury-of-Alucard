using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Fury_of_Alucard.UserInterface.Converters
{
	public class MultiplyConverter : MyMarkupExtension<MultiplyConverter>, IMultiValueConverter, IValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			double result = 1.0;
			if (parameter != null && !(parameter is double))
			{
				result = double.Parse(parameter.ToString(), System.Globalization.CultureInfo.InvariantCulture);
			}
			foreach (object v in values)
			{
				if (v is double)
				{
					result *= (double)v;
				}
			}
			return result;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (parameter != null && !(parameter is double))
			{
				parameter = double.Parse(parameter.ToString(), System.Globalization.CultureInfo.InvariantCulture);
			}
			if (value is double && parameter is double)
			{
				return (double)value * (double)parameter;
			}
			return 0.0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
