using System;
using Xunit;

namespace GreetingKata.Domain.Tests
{
    public class GreeterTests
    {
        [Theory]
        [InlineData("Mike", "Hello, Mike")]
        [InlineData("Bob", "Hello, Bob")]
        [InlineData(null, "Hello, my friend")]
        [InlineData("BOB", "HELLO BOB!")]
        [InlineData("MIKE", "HELLO MIKE!")]
        public void Greet_ShouldReturnCorrectResponse(string name, string expectedResponse)
        {
            var greeter = new Greeter();
            var response = greeter.Greet(name);
            
            Assert.Equal(expectedResponse, response);   
        }
    }
}