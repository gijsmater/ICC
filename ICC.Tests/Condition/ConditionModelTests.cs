using ICC.Condition;
using System;
using Xunit;

namespace ICC.Tests.Condition
{
    public class ConditionModelTests
    {
        [Theory]
        [InlineData("01234")]
        [InlineData("0123456789")]
        public void DiscountGroupFormatted_LengthIs20WithPadRightSpaces_ShouldPass(string discountGroup)
        {
            //Arrange
            var condition = new ConditionModel(
                discountGroup: discountGroup,
                productCode: string.Empty,
                description: string.Empty,
                discount1: 0,
                discount2: 0,
                discount3: 0,
                nettoPrice: 0,
                startDate: DateTime.Now,
                endDate: null
                );

            //Act
            var result = condition.DiscountGroupFormatted;

            //Assert
            Assert.Equal(20, result.Length);
            Assert.Contains(ICC.Constants.EMPTY_CHAR, result);
        }

        [Theory]
        [InlineData("01234")]
        [InlineData("0123456789")]
        public void ProductCodeFormatted_LengthIs20WithPadRightSpaces_ShouldPass(string productCode)
        {
            //Arrange
            var condition = new ConditionModel(
                discountGroup: string.Empty,
                productCode: productCode,
                description: string.Empty,
                discount1: 0,
                discount2: 0,
                discount3: 0,
                nettoPrice: 0,
                startDate: DateTime.Now,
                endDate: null
                );

            //Act
            var result = condition.ProductCodeFormatted;

            //Assert
            Assert.Equal(20, result.Length);
            Assert.Contains(ICC.Constants.EMPTY_CHAR, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("shortdescription")]
        [InlineData("longer description with spaces")]
        public void DescriptionFormatted_LengthIs20WithPadRightSpaces_ShouldPass(string description)
        {
            //Arrange
            var condition = new ConditionModel(
                discountGroup: string.Empty,
                productCode: string.Empty,
                description: description,
                discount1: 0,
                discount2: 0,
                discount3: 0,
                nettoPrice: 0,
                startDate: DateTime.Now,
                endDate: null
                );

            //Act
            var result = condition.DescriptionFormatted;

            //Assert
            Assert.Equal(50, result.Length);
            Assert.Contains(ICC.Constants.EMPTY_CHAR, result);
        }

        [Fact]
        public void DiscountFormatted_LengthIs5WithPadLeft_ShouldPass()
        {
            //Arrange
            int discount1 = 1;
            int discount2 = 99;
            int discount3 = 100;

            var condition = new ConditionModel(
                discountGroup: string.Empty,
                productCode: string.Empty,
                description: string.Empty,
                discount1: discount1,
                discount2: discount2,
                discount3: discount3,
                nettoPrice: 0,
                startDate: DateTime.Now,
                endDate: null
                );

            //Act
            string resultDiscount1 = condition.Discount1Formatted;
            string resultDiscount2 = condition.Discount2Formatted;
            string resultDiscount3 = condition.Discount3Formatted;

            //Assert
            Assert.NotNull(resultDiscount1);
            Assert.NotNull(resultDiscount2);
            Assert.NotNull(resultDiscount3);

            Assert.Equal(5, resultDiscount1?.Length);
            Assert.Equal(5, resultDiscount2?.Length);
            Assert.Equal(5, resultDiscount3?.Length);

            Assert.Contains(ICC.Constants.ZERO_CHAR, resultDiscount1 ?? string.Empty);
            Assert.Contains(ICC.Constants.ZERO_CHAR, resultDiscount2 ?? string.Empty);
            Assert.Contains(ICC.Constants.ZERO_CHAR, resultDiscount3 ?? string.Empty);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(1)]
        [InlineData(99999)]
        public void NettoPriceFormatted_LengthIs9WithPaddLeft_ShouldPass(int nettoPrice)
        {
            //Arrange
            var condition = new ConditionModel(
                discountGroup: string.Empty,
                productCode: string.Empty,
                description: string.Empty,
                discount1: 0,
                discount2: 0,
                discount3: 0,
                nettoPrice: nettoPrice,
                startDate: DateTime.Now,
                endDate: null
                );

            //Act
            var result = condition.NettoPriceFormatted;

            //Assert
            Assert.Equal(9, result?.Length);
            Assert.Contains(ICC.Constants.ZERO_CHAR, result ?? string.Empty);
        }

        [Fact]
        public void StartDateFormatted_IsCorrectDateFormat_ShouldPass()
        {
            //Arrange
            var condition = new ConditionModel(
                discountGroup: string.Empty,
                productCode: string.Empty,
                description: string.Empty,
                discount1: 0,
                discount2: 0,
                discount3: 0,
                nettoPrice: 0,
                startDate: new DateTime(1993, 4, 9),
                endDate: null
                );


            //Act
            var result = condition.StartDateFormatted;

            //Assert
            Assert.Equal(8, result.Length);
            Assert.Equal("19930409", result);
        }

        [Fact]
        public void EndDateFormatted_NullValueReturnsEmptyChars_ShouldPass()
        {
            //Arrange
            var condition = new ConditionModel(
                discountGroup: string.Empty,
                productCode: string.Empty,
                description: string.Empty,
                discount1: 0,
                discount2: 0,
                discount3: 0,
                nettoPrice: 0,
                startDate: new DateTime(1993, 4, 9),
                endDate: null
                );

            //Act
            var result = condition.EndDateFormatted;

            //Assert
            Assert.Equal("        ", result);
        }

        [Fact]
        public void EndDateFormated_ReturnsCorrectFormat_ShouldPass()
        {
            //Arrange
            var condition = new ConditionModel(
                discountGroup: string.Empty,
                productCode: string.Empty,
                description: string.Empty,
                discount1: 0,
                discount2: 0,
                discount3: 0,
                nettoPrice: 0,
                startDate: new DateTime(1993, 4, 9),
                endDate: new DateTime(2024, 4, 9)
                );

            //Act
            var result = condition.EndDateFormatted;

            //Assert
            Assert.Equal("20240409", result);
        }


        [Fact]
        public void ValidateModel_DiscountGroupAndProductCode_ShouldThrow()
        {
            //Arrange
            var action = () =>
            {
                var condition = new ConditionModel(
                    discountGroup: "group",
                    productCode: "code",
                    description: string.Empty,
                    discount1: null,
                    discount2: null,
                    discount3: null,
                    nettoPrice: null,
                    startDate: new DateTime(1993, 4, 9),
                    endDate: new DateTime(2024, 4, 9)
                    );
            };

            //Act
            var exception = Assert.Throws<InvalidDataException>(action);

            //Assert
            Assert.Equal("A condition line should contain a discount group OR a product code, not both.", exception.Message); ;
        }


        [Fact]
        public void ValidateModel_DiscountGroupShouldHaveOneDiscount_ShouldThrow()
        {
            //Arrange
            var action = () =>
            {
                var condition = new ConditionModel(
                    discountGroup: "group",
                    productCode: string.Empty,
                    description: string.Empty,
                    discount1: null,
                    discount2: null,
                    discount3: null,
                    nettoPrice: null,
                    startDate: new DateTime(1993, 4, 9),
                    endDate: new DateTime(2024, 4, 9)
                    );
            };

            //Act
            var exception = Assert.Throws<InvalidDataException>(action);

            //Assert
            Assert.Equal("When using a discount group atleast one discount percentage should be given.", exception.Message); ;
        }


        [Fact]
        public void ValidateModel_ProdcutCodeShouldHaveOneDiscountOrNettoPrice_ShouldThrow()
        {
            //Arrange
            var action = () =>
            {
                var condition = new ConditionModel(
                    discountGroup: string.Empty,
                    productCode: "code",
                    description: string.Empty,
                    discount1: null,
                    discount2: null,
                    discount3: null,
                    nettoPrice: null,
                    startDate: new DateTime(1993, 4, 9),
                    endDate: new DateTime(2024, 4, 9)
                    );
            };

            //Act
            var exception = Assert.Throws<InvalidDataException>(action);

            //Assert
            Assert.Equal("When using a product code atleast one discount percentage or the netto price should be given.", exception.Message); ;
        }


        [Fact]
        public void NullableIntsFormatted_ReturnPadLeftString_ShouldPass()
        {
            //Arrange
            var condition1 = new ConditionModel(
                    discountGroup: string.Empty,
                    productCode: "code",
                    description: string.Empty,
                    discount1: null,
                    discount2: null,
                    discount3: null,
                    nettoPrice: 0,
                    startDate: new DateTime(1993, 4, 9),
                    endDate: new DateTime(2024, 4, 9)
                    );

            var condition2 = new ConditionModel(
                    discountGroup: "group",
                    productCode: string.Empty,
                    description: string.Empty,
                    discount1: 0,
                    discount2: null,
                    discount3: null,
                    nettoPrice: null,
                    startDate: new DateTime(1993, 4, 9),
                    endDate: new DateTime(2024, 4, 9)
                    );

            //Act
            //Assert
            Assert.Equal("00000", condition1.Discount1Formatted);
            Assert.Equal("00000", condition1.Discount2Formatted);
            Assert.Equal("00000", condition1.Discount3Formatted);
            Assert.Equal("000000000", condition1.NettoPriceFormatted);

            Assert.Equal("00000", condition2.Discount1Formatted);
            Assert.Equal("00000", condition2.Discount2Formatted);
            Assert.Equal("00000", condition2.Discount3Formatted);
            Assert.Equal("000000000", condition2.NettoPriceFormatted);
        }


    }
}
