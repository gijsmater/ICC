namespace ICC.Tests
{
    public class ValidationTests
    {
        [Fact]
        public void IsValidMaxLength_ThrowsWhenExceedsMaxLength_ShouldThrow()
        {
            //Arrange
            string value = "123456";
            int maxLength = 5;

            //Act
            var action = () => value.IsValidMaxLength(maxLength);

            //Assert
            var exception = Assert.Throws<InvalidDataException>(action);
            Assert.Equal($"Max length of value is {maxLength}, actual length is {value.Length}", exception.Message);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsValidGln_ThrowsWhenGlnIsNullOrEmpty_ShouldThrow(string gln)
        {
            //Arrange
            //Act
            var action = () => gln.IsValidGln();

            //Assert
            var exception = Assert.Throws<InvalidDataException>(action);

            Assert.Equal("A GLN cant be NULL or empty", exception.Message);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("12345678912345")]
        [InlineData("123456789123456789")]
        public void IsValidGln_ThrowsWhenLengthIsIncorrect_ShouldThrow(string gln)
        {
            //Arrange
            //Act
            var action = () => gln.IsValidGln();

            //Assert
            var exception = Assert.Throws<InvalidDataException>(action);

            Assert.Equal($"Expected length of GLN is 13, actual length is {gln.Length}", exception.Message);
        }
    }
}
