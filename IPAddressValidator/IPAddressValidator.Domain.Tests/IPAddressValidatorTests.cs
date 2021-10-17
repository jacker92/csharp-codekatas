using System;
using Xunit;

namespace IPAddressValidator.Domain.Tests
{
    public class IPAddressValidatorTests
    {
        private readonly IPAddressValidator _validator;

        public IPAddressValidatorTests()
        {
            _validator = new IPAddressValidator();
        }

        [Fact]
        public void ValidateIpv4Address_ShouldThrowArgumentException_WithWhitespaceInput()
        {
            Assert.Throws<ArgumentException>(() => _validator.ValidateIpv4Address(" "));
        }

        [Theory]
        [InlineData("1.1.1.1", true)]
        [InlineData("192.168.1.1", true)]
        [InlineData("10.0.0.1", true)]
        [InlineData("127.0.0.1", true)]
        [InlineData("0.0.0.0", false)]
        [InlineData("255.255.255.255", false)]
        [InlineData("1.1.1.0", false)]
        [InlineData("10.0.1", false)]
        [InlineData("AB.C.D.E", false)]
        public void ValidateIpv4Address_ShouldReturnCorrectResult(string ipAddress, bool expected)
        {
            var result = _validator.ValidateIpv4Address(ipAddress);
            Assert.Equal(expected, result);
        }
    }
}
