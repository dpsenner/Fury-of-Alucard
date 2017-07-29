using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace Fury_of_Alucard.UserInterface.Converters
{
	public class PointsToPointCollectionConverter : MyMarkupExtension<PointsToPointCollectionConverter>, IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			PointCollection result = new PointCollection();

			List<Point> points = values[0] as List<Point>;
			if (points == null)
				return result;
			if (!(values[1] is double))
				return result;
			if (!(values[2] is double))
				return result;
			if (!(values[3] is double))
				return result;
			if (!(values[4] is double))
				return result;

			double width = (double)values[1];
			double height = (double)values[2];
			double x = (double)values[3];
			double y = (double)values[4];
			if (width > 0 && height > 0 && x > 0 && y > 0)
			{
				foreach (Point p in points)
				{
					result.Add(new Point((p.X - x) * width, (p.Y - y) * height));
				}
			}
			return result;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
