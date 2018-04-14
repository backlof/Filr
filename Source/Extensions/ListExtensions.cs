using System;
using System.Collections.Generic;

namespace Extensions
{
	public static class ListExtensions
	{
		public static T Random<T>(this List<T> list)
		{
			int count = list.Count;
			return count == 0 ? default(T) : list[new Random().Next(0, count)];
		}
	}
}