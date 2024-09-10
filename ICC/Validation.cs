using System.IO;

namespace ICC
{
    public static class Validation
    {
        public static string IsValidMaxLength(this string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                throw new InvalidDataException(
                    $"Max length of value is {maxLength}, actual length is {value.Length}");
            }
            return value;
        }

        public static string IsValidGln(this string gln)
        {
            if (string.IsNullOrEmpty(gln))
            {
                throw new InvalidDataException("A GLN cant be NULL or empty");
            }

            if (gln.Length != 13)
            {
                throw new InvalidDataException($"Expected length of GLN is 13, actual length is {gln.Length}");
            }
            return gln;
        }
    }
}
