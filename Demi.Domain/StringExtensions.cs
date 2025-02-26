using System.Globalization;

namespace Demi.Domain
{
	public static class StringExtensions
	{
		/// <summary>
		///	Splits a string by newlines, trims each line, and filters out empty lines.
		/// </summary>
		/// <param name="str"></param>
		/// <returns>
		///	An array of strings, each representing a non-empty line in the original string.
		/// </returns>
		public static string[] SplitTrimAndFilterLines(this string str)
		{
			return str.Split('\n').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
		}
	}
}

