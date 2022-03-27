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

        [Theory]
        [InlineData("Jill", "Jane", "Hello, Jill and Jane")]
        public void Greet_ShouldReturnCorrectResponseForArrayOfNames(string input1, string input2, string expectedResponse)
        {
            var greeter = new Greeter();
            var response = greeter.Greet(new string[] {input1, input2});
            Assert.Equal(response, expectedResponse);
        }
    }
}