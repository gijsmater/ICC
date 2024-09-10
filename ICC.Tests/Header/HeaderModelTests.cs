using ICC.Condition;
using ICC.Document;
using ICC.Header;
using System;
using System.Globalization;
using Xunit;

namespace ICC.Tests.Header
{
    public class HeaderModelTests
    {
        [Theory]
        [InlineData("8899")]//4 chars
        [InlineData("0123456789")]//10 chars
        [InlineData("0123456789012345678")]//19 chars
        public void CustomerIdFormatted_LengthIs20WithPaddedSpace_ShouldPass(string customerId)
        {
            //Arrange
            var header = new HeaderModel(
                supplierGLN: Constants.EXAMPLE_GLN,
                supplierName: string.Empty,
                customerId: customerId,
                customerGLN: Constants.EXAMPLE_GLN,
                productionDate: DateTime.Now,
                amountOfRows: 0,
                versionNumber: string.Empty);

            //Act
            string result = header.CustomerIdFormatted;

            //Assert
            Assert.Equal(20, result.Length);
            Assert.Contains(ICC.Constants.EMPTY_CHAR, result);
        }


        [Fact]
        public void ProductionDateFormatted_Format_ShouldPass()
        {
            //Arrange
            var header = new HeaderModel(
                supplierGLN: Constants.EXAMPLE_GLN,
                supplierName: string.Empty,
                customerId: string.Empty,
                customerGLN: Constants.EXAMPLE_GLN,
                productionDate: new DateTime(1993, 4, 9),
                amountOfRows: 0,
                versionNumber: string.Empty);

            //Act
            string productionDate = header.ProductionDateFormatted;

            //Assert
            Assert.Equal("19930409", productionDate);
        }


        [Theory]
        [InlineData(25)]
        [InlineData(1111)]
        [InlineData(12345)]
        [InlineData(000001)]
        public void AmountOfRowsFormatted_IsAlwaysLengthOfSixWithPadLefZeros_ShouldPass(int amount)
        {
            //Arrange
            var header = new HeaderModel(
                supplierGLN: Constants.EXAMPLE_GLN,
                supplierName: string.Empty,
                customerId: string.Empty,
                customerGLN: Constants.EXAMPLE_GLN,
                productionDate: DateTime.Now,
                amountOfRows: amount,
                versionNumber: string.Empty);

            //Act
            string result = header.AmountOfRowsFormatted;

            //Assert
            Assert.Equal(6, result.Length);
            Assert.Contains(ICC.Constants.ZERO_CHAR, result);
        }


        [Theory]
        [InlineData("1.1")]
        [InlineData("1")]
        [InlineData("1234")]
        public void VersionNumberFormatted_IsCorrectLengthWithPadRight_ShouldPass(string version)
        {
            //Arrange
            var header = new HeaderModel(
                supplierGLN: Constants.EXAMPLE_GLN,
                supplierName: string.Empty,
                customerId: string.Empty,
                customerGLN: Constants.EXAMPLE_GLN,
                productionDate: DateTime.Now,
                amountOfRows: 0,
                versionNumber: version);

            //Act
            var result = header.VersionNumberFormatted;

            //Assert
            Assert.Equal(5, result.Length);
            Assert.Contains(ICC.Constants.EMPTY_CHAR, result);
        }


        [Theory]
        [InlineData("")]
        [InlineData("short name")]
        [InlineData("0123456789012345678901234567890123")]
        public void SupplierNameFormatted_IsCorrectLengthWithPadRight_ShouldPass(string supplierName)
        {
            //Arrange
            var header = new HeaderModel(
                supplierGLN: Constants.EXAMPLE_GLN,
                supplierName: supplierName,
                customerId: string.Empty,
                customerGLN: Constants.EXAMPLE_GLN,
                productionDate: DateTime.Now,
                amountOfRows: 0,
                versionNumber: string.Empty);

            //Act
            var result = header.SupplierNameFormatted;

            //Assert
            Assert.Equal(35, result.Length);
            Assert.Contains(ICC.Constants.EMPTY_CHAR, result);
        }


        [Fact]
        public void CustomerGlnFormatted_IsValid_ShouldPass()
        {
            //Arrange
            var header = new HeaderModel(
                supplierGLN: Constants.EXAMPLE_GLN,
                supplierName: string.Empty,
                customerId: string.Empty,
                customerGLN: Constants.EXAMPLE_GLN,
                productionDate: DateTime.Now,
                amountOfRows: 0,
                versionNumber: string.Empty);

            //Act
            var result = header.CustomerGlnFormatted;

            //Assert
            Assert.Equal(Constants.EXAMPLE_GLN, result);
        }


        [Fact]
        public void ToString_GivesValidHeaderRow_ShouldPass()
        {
            //Arrange
            string supplierName = "Example Company GmbH";
            string supplierGLN = Constants.EXAMPLE_GLN;
            string customerId = "1001";
            string customerGln = "8111113000001";
            var productionDate = DateTime.Now;
            int amountOfRows = 100;
            string versionNumber = "1.1";

            var header = new HeaderModel(
                supplierGLN: supplierGLN,
                supplierName: supplierName,
                customerId: customerId,
                customerGLN: customerGln,
                productionDate: productionDate,
                amountOfRows: amountOfRows,
                versionNumber: versionNumber);

            //Act
            string result = header.ToString();

            //Assert
            Assert.Contains(supplierGLN, result);
            Assert.Contains(supplierName, result);
            Assert.Contains(customerId, result);
            Assert.Contains(customerGln, result);
            Assert.Contains(productionDate.ToString(ICC.Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture), result);
            Assert.Contains(amountOfRows.ToString(), result);
            Assert.Contains(customerId, result);
            Assert.Contains(versionNumber, result);
            Assert.Equal(132, result.Length);
        }
    }
}
