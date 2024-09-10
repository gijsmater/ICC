using System;
using System.Globalization;
using System.IO;

namespace ICC.Header
{
    public static class HeaderExtensions
    {
        public static HeaderModel ToHeaderModel(this string value)
        {
            if (value == null || string.IsNullOrEmpty(value))
            {
                throw new InvalidDataException("No head line found.");
            }

            string amountOfRowsString = value.Substring(48, 6);
            if (!int.TryParse(amountOfRowsString, out int amountOfRows))
            {
                throw new InvalidDataException(
                    $"Amount of rows is expected to be an interger. Actual value: {amountOfRowsString}");
            }

            string productionDateString = value.Substring(40, 8);
            if (!DateTime.TryParseExact(
                productionDateString, 
                Constants.DATETIME_FORMAT, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out DateTime productionDate))
            {
                throw new InvalidDataException($"Production date is expected to be a date. Actual value: {productionDateString}");
            }

            string supplierGln = value.Substring(0, 13).Trim();
            string supplierName = value.Substring(59, 35).Trim();
            string customerId = value.Substring(20, 20).Trim();
            string customerGln = value.Substring(94, 13).Trim();
            string versionNumber = value.Substring(54, 4).Trim();

            return new HeaderModel(
                supplierGln,
                supplierName,
                customerId,
                customerGln,
                productionDate,
                amountOfRows,
                versionNumber);
        }
    }
}
