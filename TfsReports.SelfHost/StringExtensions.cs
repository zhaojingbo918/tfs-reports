namespace TfsReports.SelfHost
{
    public static class StringExtensions
    {
        public static string With(this string format, params object[] parameters)
        {
            return string.Format(format, parameters);
        }

        public static string EncloseInDoubleQuotes(this string input)
        {
            return "\"" + input + "\"";
        }

        public static string ReplaceCrLfWithSpace(this string input)
        {
            return input.Replace("\r\n", " ");
        }
    }
}