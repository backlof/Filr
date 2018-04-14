using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions
{
	public static class IEnumerableExtensions
	{
		public static T Random<T>(this IEnumerable<T> enumerable)
		{
			int count = enumerable.Count();
			return count == 0 ? default(T) : enumerable.ElementAt(new Random().Next(0, count));
		}
	}
}
