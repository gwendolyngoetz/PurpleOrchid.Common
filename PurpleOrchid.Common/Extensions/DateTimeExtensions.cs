namespace PurpleOrchid.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToShortDateString(this DateTime? source)
        {
            return source == null ? string.Empty : source.Value.ToString("d");
        }
    }
}
