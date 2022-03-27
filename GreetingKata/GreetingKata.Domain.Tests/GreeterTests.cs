using System;
using Xunit;

namespace GreetingKata.Domain.Tests
{
    public class GreeterTests
    {
        [Fact]
        public void Greet_ShouldThrowArgumentException_WithEmptyName()
        {
            var greeter = new Greeter();
            Assert.Throws<ArgumentException>(() => greeter.Greet(string.Empty));
        }

        [Theory]
        [InlineData("Mike", "Hello, Mike")]
        [InlineData("Bob", "Hello, Bob")]
        public void Greet_ShouldReturnCorrectResponse(string name, string expectedResponse)
        {
            var greeter = new Greeter();
            var response = greeter.Greet(name);
            
            Assert.Equal(expectedResponse, response);   
        }

    }
}