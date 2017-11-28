using CreditCards.Core.Model;
using Xunit;

namespace CreditCards.Tests.Model
{
    public class FrequentFlyerNumberValidatorShould
    {
        [Theory]
        [InlineData("012345-A")]
        [InlineData("012345-Q")]
        [InlineData("012345-Y")]
        public void AcceptValidSchemes(string number)
        {
            var sut = new FrequentFlyerNumberValidator();

            Assert.True(sut.IsValid(number));
        }

        [Fact]
        public void NotAcceptValidSchemes()
        {
            var sut = new FrequentFlyerNumberValidator();

            Assert.False(sut.IsValid("012345-B"));
        }
    }
}
