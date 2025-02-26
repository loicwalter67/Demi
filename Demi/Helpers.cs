using System.Globalization;

namespace Demi
{
	public static class Helpers
	{
		public static string ToLowerString(this Enum state)
		{
			return state.ToString().ToLower();
		}

		public static string GetLongDatePatternEN(this DateTime date)
		{
			CultureInfo culture = new("en-US");
			return date.ToString(culture.DateTimeFormat.LongDatePattern, culture);
		}
	}
}
