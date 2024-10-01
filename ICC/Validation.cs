using System.IO;

namespace ICC
{
    public static class Validation
    {
        public static string IsValidMaxLength(this string value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value) &&
                value.Length > maxLength)
            {
                throw new InvalidDataException(
                    $"Max length of value is {maxLength}, actual length is {value.Length}");
            }
            return value;
        }

        public static string IsValidGln(this string gln)
        {
            if (!string.IsNullOrEmpty(gln) &&//GLN is not mandatory
                gln.Length != 13)
            {
                throw new InvalidDataException($"Expected length of GLN is 13, actual length is {gln.Length}");
            }
            return gln;
        }
    }
}
