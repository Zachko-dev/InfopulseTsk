using System;
using System.Collections.Generic;
using System.Linq;

namespace Rectangle.Impl
{
	public static class Service
	{
		/// <summary>
		/// See TODO.txt file for task details.
		/// Do not change contracts: input and output arguments, method name and access modifiers
		/// </summary>
		/// <param name="points"></param>
		/// <returns></returns>
		public static Rectangle FindRectangle(List<Point> points)
		{
			if (points == null)
				throw new ArgumentNullException("points", "is null");

			if (points.Count < 2)
				throw new ArgumentException("you should pass at least 2 points", "points");

			if (!ValidateUniqueness(points))
				throw new ArgumentException("All points should be unique", "points");

			// чтобы исключить какую-то одну точку, она должна быть крайней по игреку(либо по иксу),
			// но при этом не должно быть точек с такой же координатой по игреку(или по иксу соотв.)
			// поэтому по очереди проверим в каждую сторону(влево, вправо, вниз и вверх)
			// если такой точки нету, то мы не сможем построить прямоугольник

			var minimalX = points.Min(point => point.X);
			var minimalY = points.Min(point => point.Y);
			var maximumX = points.Max(point => point.X);
			var maximumY = points.Max(point => point.Y);

			var minXPoints = points.Where(point => point.X == minimalX);
			if (minXPoints.Count() == 1)
				return BuildRectangle(points, minXPoints.First());

			var maxXPoints = points.Where(point => point.X == maximumX);
			if (maxXPoints.Count() == 1)
				return BuildRectangle(points, maxXPoints.First());

			var minYPoints = points.Where(point => point.Y == minimalY);
			if (minYPoints.Count() == 1)
				return BuildRectangle(points, minYPoints.First());

			var maxYPoints = points.Where(point => point.Y == maximumY);
			return BuildRectangle(points, maxYPoints.First());

			throw new ArgumentException("Invalid points", "points");
		}

		/// <summary>
		/// check if list of points has points with same coordinates
		/// </summary>
		/// <param name="points"></param>
		/// <returns>true if validdation is succesfull</returns>
		public static bool ValidateUniqueness(List<Point> points)
		{
			var length = points.Count;

			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length; j++)
				{
					if (i == j)
						continue;

					if (points[i].X == points[j].X && points[i].Y == points[j].Y)
						return false;
				}
			}

			return true;
		}

		public static Rectangle BuildRectangle(List<Point> points, Point pointToExclude)
        {
			points.Remove(pointToExclude);

			// если в points остается только одна точка, то добавляем еще одну,
			// чтобы при этом не зацепить точку, которую мы исключаем.
			// поэтому вычисляем зеркальную точку от той, которую нужно исключить, относительно единственной точке в points
			if (points.Count == 1)
            {
				var newPoint = new Point()
				{
					X = points.First().X + (points.First().X - pointToExclude.X),
					Y = points.First().Y + (points.First().Y - pointToExclude.Y)
				};
				points.Add(newPoint);
            }

			var minX = points.Min(point => point.X);
			var minY = points.Min(point => point.Y);
			var maxX = points.Max(point => point.X);
			var maxY = points.Max(point => point.Y);

			var rectX = (int)Math.Ceiling( (maxX + minX) / 2.0);
			var rectY = (int)Math.Ceiling( (maxY + minY) / 2.0);

			return new Rectangle()
			{
				X = rectX,
				Y = rectY,
				Width = maxX - minX,
				Height = maxY - minY
			};
		}
	}
}
