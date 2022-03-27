using System;
using Xunit;

namespace GreetingKata.Domain.Tests
{
    public class GreeterTests
    {
        private readonly Greeter _greeter;

        public GreeterTests()
        {
            _greeter = new Greeter();
        }

        [Theory]
        [InlineData("Mike", "Hello, Mike")]
        [InlineData("Bob", "Hello, Bob")]
        [InlineData(null, "Hello, my friend")]
        [InlineData("BOB", "HELLO BOB!")]
        [InlineData("MIKE", "HELLO MIKE!")]
        public void Greet_ShouldReturnCorrectResponse(string name, string expectedResponse)
        {
            var response = _greeter.Greet(name);
            
            Assert.Equal(expectedResponse, response);   
        }

        [Theory]
        [InlineData("Jill", "Jane", "Hello, Jill and Jane")]
        public void Greet_ShouldReturnCorrectResponseForArrayOfTwoNames(string input1, string input2, string expectedResponse)
        {
            var response = _greeter.Greet(new string[] {input1, input2});
            Assert.Equal(expectedResponse, response);
        }


        [Theory]
        [InlineData("Amy", "Brian", "Charlotte", "Hello, Amy, Brian, and Charlotte")]
        [InlineData("Amy", "BRIAN", "Charlotte", "Hello, Amy and Charlotte. AND HELLO BRIAN!")]
        public void Greet_ShouldReturnCorrectResponseForArrayOfThreeNames(string input1, string input2, string input3, string expectedResponse)
        {
            var response = _greeter.Greet(new string[] { input1, input2, input3 });
            Assert.Equal(expectedResponse, response);
        }
    }
}