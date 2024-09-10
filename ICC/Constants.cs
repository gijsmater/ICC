using System.Text;

namespace ICC
{
    public static class Constants
    {
        public static readonly char EMPTY_CHAR = ' ';
        public static readonly char ZERO_CHAR = '0';
        public static readonly string DATETIME_FORMAT = "yyyyMMdd";

        public static string GetNotApplicalbeString(int length)
        {
            var value = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                value.Append(EMPTY_CHAR);
            }
            return value.ToString();
        }
    }
}
