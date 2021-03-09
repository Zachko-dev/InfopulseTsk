using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rectangle.Impl;

namespace Rectangle.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void FindRectangleThrowsExceptionIfArgumentIsNull()
		{
			try
            {
				var rectangle = Service.FindRectangle(null);
			}
			catch (ArgumentNullException)
            {
				Assert.True(true);
            }
			
			Assert.False(false);
		}

		[Test]
		public void FindRectangleThrowsExceptionIfPointsContainSingleElement()
		{
			try
			{
				var testPoints = new List<Point>() {
					new Point()
				};
				var rectangle = Service.FindRectangle(testPoints);
			}
			catch (ArgumentException)
			{
				Assert.True(true);
			}

			Assert.False(false);
		}

		[Test]
		public void FindRectangleThrowsExceptionIfPointsContainDuplicateElements()
		{
			try
			{
				var testPoints = new List<Point>() {
					new Point() {X = 10, Y = 5},
					new Point() {X = 10, Y = 5}
				};
				var rectangle = Service.FindRectangle(testPoints);
			}
			catch (ArgumentException)
			{
				Assert.True(true);
			}

			Assert.False(false);
		}

		[Test]
		public void FindRectangleReturnsCorrectRectangle1()
		{
			var testPoints = new List<Point>() {
					new Point() {X = 0, Y = 5},
					new Point() {X = 5, Y = 15}
				};

			var rectangle = Service.FindRectangle(testPoints);

			Assert.AreEqual(8, rectangle.X);
			Assert.AreEqual(20, rectangle.Y);
			Assert.AreEqual(5, rectangle.Width);
			Assert.AreEqual(10, rectangle.Height);
		}

		[Test]
		public void FindRectangleReturnsCorrectRectangle2()
		{
			var testPoints = new List<Point>() {
					new Point() {X = 0, Y = 5},
					new Point() {X = 0, Y = 15},
					new Point() {X = 5, Y = 20},
					new Point() {X = 10, Y = 10}
				};

			var rectangle = Service.FindRectangle(testPoints);

			Assert.AreEqual(3, rectangle.X);
			Assert.AreEqual(13, rectangle.Y);
			Assert.AreEqual(5, rectangle.Width);
			Assert.AreEqual(15, rectangle.Height);
		}

		[Test]
		public void FindRectangleThrowsArgumentException()
		{
			var testPoints = new List<Point>() {
					new Point() {X = 30, Y = 5},
					new Point() {X = 0, Y = 35},
					new Point() {X = 10, Y = 35},
					new Point() {X = 0, Y = 15},
					new Point() {X = 30, Y = 20},
					new Point() {X = 20, Y = 5}
				};
			try
            {
				var rectangle = Service.FindRectangle(testPoints);
			}
			catch (ArgumentException)
            {
				Assert.True(true);
            }

			Assert.False(false);
		}
	}
}