using System;
using System.Globalization;
using System.IO;

namespace ICC.Condition
{
    public static class ConditionExtensions
    {
        public static ConditionModel ToConditionModel(this string value)
        {
            string discountGroup = value.Substring(0, 20).Trim();
            string productCode = value.Substring(20, 20).Trim();
            string description = value.Substring(40, 50).Trim();
            string discount1String = value.Substring(90, 5).Trim();
            string discount2String = value.Substring(95, 5).Trim();
            string discount3String = value.Substring(100, 5).Trim();
            string nettoPriceString = value.Substring(105, 9).Trim();
            string startDateString = value.Substring(114, 8).Trim();
            string endDateString = value.Substring(122, 8).Trim();

            int? discount1 = int.TryParse(discount1String, out int tempDiscount1) ? tempDiscount1 : (int?)null;
            int? discount2 = int.TryParse(discount2String, out int tempDiscount2) ? tempDiscount2 : (int?)null;
            int? discount3 = int.TryParse(discount3String, out int tempDiscount3) ? tempDiscount3 : (int?)null;
            int? nettoPrice = int.TryParse(nettoPriceString, out int tempNettoPrice) ? tempNettoPrice : (int?)null;
            DateTime? endDate = DateTime.TryParse(endDateString, out DateTime endDateTemp) ? endDateTemp : (DateTime?)null ;

            if(!DateTime.TryParseExact(
                startDateString,
                Constants.DATETIME_FORMAT,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime startDate))
            {
                throw new InvalidDataException($"Start date excpected to be a date. Actual value {startDateString}");
            }

            return new ConditionModel(
                discountGroup,
                productCode,
                description,
                discount1,
                discount2,
                discount3,
                nettoPrice,
                startDate,
                endDate);
        }
    }
}
