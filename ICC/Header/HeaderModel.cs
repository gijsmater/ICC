using System;
using System.Globalization;

namespace ICC.Header
{
    public class HeaderModel
    {
        /// <summary>
        /// GLN of the supplier
        /// Format: A
        /// Length: 13
        /// Start:  1
        /// End:    13
        /// </summary>
        private string SupplierGLN { get; set; }


        /// <summary>
        /// Name of the supplier
        /// Format: A
        /// Length: 35
        /// Start:  60
        /// End:    94
        /// </summary>
        private string SupplierName { get; set; }

        /// <summary>
        /// Debtor number of the customer
        /// Format: A
        /// Length: 20
        /// Start:  21
        /// End:    40
        /// </summary>
        private string CustomerId { get; set; }

        /// <summary>
        /// The GLN of the customer
        /// Format: A
        /// Length: 13
        /// Start:  95
        /// End:    107
        /// </summary>
        private string CustomerGLN { get; set; }

        /// <summary>
        /// Date of creation of the file
        /// Format: JJJJMMDD
        /// Length: 8
        /// Start:  41
        /// End:    48
        /// </summary>
        private DateTime ProductionDate { get; set; }

        /// <summary>
        /// Amount of condition rows (excluding this header row)
        /// Format: Z
        /// Length: 6
        /// Start:  49
        /// End:    54
        /// </summary>
        private int AmountOfRows { get; set; }

        /// <summary>
        /// Version of the condition file (1.1)
        /// Format: A
        /// Length: 5
        /// Start:  55
        /// End:    59
        /// </summary>
        private string VersionNumber { get; set; }


        /// <summary>
        /// The header of a file. This line can occur 1 time.
        /// </summary>
        /// <param name="supplierGLN">GLN of the supplier</param>
        /// <param name="supplierName">The name of the supplier</param>
        /// <param name="customerId">Debtor number</param>
        /// <param name="customerGLN">GLN of the debtor</param>
        /// <param name="productionDate">Date of file creation</param>
        /// <param name="amountOfRows">Amount of rows in the file (excluding this header)</param>
        /// <param name="versionNumber">Version Number of ICC (1.1)</param>
        public HeaderModel(
            string supplierGLN,
            string supplierName,
            string customerId,
            string customerGLN,
            DateTime productionDate,
            int amountOfRows,
            string versionNumber)
        {
            SupplierGLN = supplierGLN
                .IsValidGln()
                .IsValidMaxLength(13);
            SupplierName = supplierName.IsValidMaxLength(35);
            CustomerId = customerId.IsValidMaxLength(20);
            CustomerGLN = customerGLN
                .IsValidGln()
                .IsValidMaxLength(13);
            ProductionDate = productionDate;
            AmountOfRows = amountOfRows;
            VersionNumber = versionNumber.IsValidMaxLength(5);
        }

        public string SupplierGlnFormatted => SupplierGLN;
        public string CustomerIdFormatted => CustomerId.PadRight(20, Constants.EMPTY_CHAR);
        public string ProductionDateFormatted => ProductionDate.ToString(Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
        public string AmountOfRowsFormatted => AmountOfRows.ToString().PadLeft(6, Constants.ZERO_CHAR);
        public string VersionNumberFormatted => VersionNumber.PadRight(5, Constants.EMPTY_CHAR);
        public string SupplierNameFormatted => SupplierName.PadRight(35, Constants.EMPTY_CHAR);
        public string CustomerGlnFormatted => CustomerGLN;

        public override string ToString()
        {
            string na1 = Constants.GetNotApplicalbeString(7);
            string na2 = Constants.GetNotApplicalbeString(23);

            return SupplierGlnFormatted +
                na1 +
                CustomerIdFormatted +
                ProductionDateFormatted +
                AmountOfRowsFormatted +
                VersionNumberFormatted +
                SupplierNameFormatted +
                CustomerGlnFormatted +
                na2 +
                "\r\n";
        }
    }
}
