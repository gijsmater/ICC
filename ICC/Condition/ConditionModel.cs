using System;
using System.Globalization;
using System.IO;

namespace ICC.Condition
{
    public class ConditionModel
    {
        /// <summary>
        /// If this is filled, dont fill in the product code.
        /// Format: A
        /// Length: 20
        /// Start:  1
        /// End:    20
        /// </summary>
        private string DiscountGroup { get; set; }

        /// <summary>
        /// If this is filled, dont fill the discount group
        /// Format: A
        /// Length: 20
        /// Start:  21
        /// End:    40
        /// </summary>
        private string ProductCode { get; set; }

        /// <summary>
        /// Description of the discount group or the product code.
        /// Format: A
        /// Length: 50
        /// Start:  41
        /// End:    90
        /// </summary>
        private string Description { get; set; }

        /// <summary>
        /// % in hundreds without decimals
        /// Format: Z
        /// Length: 5
        /// Start:  91
        /// End:    95
        /// </summary>
        private int? Discount1 { get; set; }

        /// <summary>
        /// % in hundreds without decimals
        /// Format: Z
        /// Length: 5
        /// Start:  96
        /// End:    100
        /// </summary>
        private int? Discount2 { get; set; }

        /// <summary>
        /// % in hundreds without decimals
        /// Format: Z
        /// Length: 5
        /// Start:  101
        /// End:    105
        /// </summary>
        private int? Discount3 { get; set; }

        /// <summary>
        /// Price in eurocents.
        /// Format: Z
        /// Length: 9
        /// Start:  106
        /// End:    114
        /// </summary>
        private int? NettoPrice { get; set; }

        /// <summary>
        /// The valid start date of the discount.
        /// Format: JJJJMMDD
        /// Length: 8
        /// Start:  115
        /// End:    122
        /// </summary>
        private DateTime StartDate { get; set; }

        /// <summary>
        /// The end of the discount (optional)
        /// Format: JJJJMMDD
        /// Length: 8
        /// Start:  123
        /// End:    130
        /// </summary>
        private DateTime? EndDate { get; set; }

        /// <summary>
        /// A discount conidtion for a discount group or a product code.
        /// </summary>
        /// <param name="discountGroup">If this is filled, dont fill in the product code.</param>
        /// <param name="productCode">If this is filled, dont fill the discount group</param>
        /// <param name="description">Description of the discount group or the product code.</param>
        /// <param name="discount1">% in hundreds without decimals</param>
        /// <param name="discount2">% in hundreds without decimals</param>
        /// <param name="discount3">% in hundreds without decimals</param>
        /// <param name="nettoPrice">Price in eurocents.</param>
        /// <param name="startDate">The valid start date of the discount.</param>
        /// <param name="endDate">The end of the discount (optional)</param>
        public ConditionModel(
            string discountGroup,
            string productCode,
            string description,
            int? discount1,
            int? discount2,
            int? discount3,
            int? nettoPrice,
            DateTime startDate,
            DateTime? endDate)
        {
            DiscountGroup = discountGroup.IsValidMaxLength(20);
            ProductCode = productCode.IsValidMaxLength(20);
            Description = description.IsValidMaxLength(50);
            Discount1 = discount1;
            Discount2 = discount2;
            Discount3 = discount3;
            NettoPrice = nettoPrice;
            StartDate = startDate;
            EndDate = endDate;

            ValidateModel();
        }


        public string DiscountGroupFormatted => DiscountGroup.PadRight(20, Constants.EMPTY_CHAR);
        public string ProductCodeFormatted => ProductCode.PadRight(20, Constants.EMPTY_CHAR);
        public string DescriptionFormatted => Description.PadRight(50, Constants.EMPTY_CHAR);
        public string Discount1Formatted => Discount1?.ToString().PadLeft(5, '0');
        public string Discount2Formatted => Discount2?.ToString().PadLeft(5, '0');
        public string Discount3Formatted => Discount3?.ToString().PadLeft(5, '0');
        public string NettoPriceFormatted => NettoPrice?.ToString().PadLeft(9, '0');
        public string StartDateFormatted => StartDate.ToString(Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
        public string EndDateFormatted => 
            EndDate.HasValue ? 
            EndDate.Value.ToString(Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture) : 
            Constants.GetNotApplicalbeString(8);

        public override string ToString()
        {
            return DiscountGroupFormatted +
                ProductCodeFormatted +
                DescriptionFormatted +
                Discount1Formatted +
                Discount2Formatted +
                Discount3Formatted +
                NettoPriceFormatted +
                StartDateFormatted +
                EndDateFormatted +
                "\r\n";
        }

        private void ValidateModel()
        {
            if (!string.IsNullOrEmpty(DiscountGroup) && !string.IsNullOrEmpty(ProductCode))
            {
                throw new InvalidDataException(
                    "A condition line should contain a discount group OR a product code, not both.");
            }

            if (!string.IsNullOrEmpty(DiscountGroup) &&
                Discount1 == null && 
                Discount2 == null && 
                Discount3 == null)
            {
                throw new InvalidDataException(
                    "When using a discount group atleast one discount percentage should be given.");
            }

            if (!string.IsNullOrEmpty(ProductCode) &&
                Discount1 == null && 
                Discount2 == null && 
                Discount3 == null && 
                NettoPrice == null)
            {
                throw new InvalidDataException(
                    "When using a product code atleast one discount percentage or the netto price should be given.");
            }
        }
    }
}
