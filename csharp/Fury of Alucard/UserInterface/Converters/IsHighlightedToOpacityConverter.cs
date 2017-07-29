using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Fury_of_Alucard.UserInterface.Converters
{
	public class IsHighlightedToOpacityConverter : MyMarkupExtension<IsHighlightedToOpacityConverter>, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is bool)
			{
				if (((bool)value))
					return 0.5;
			}
			return 0.1;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
