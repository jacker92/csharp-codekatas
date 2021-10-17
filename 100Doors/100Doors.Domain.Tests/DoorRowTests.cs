using _100Doors.Domain.Models;
using System;
using Xunit;

namespace _100Doors.Console.Tests
{
    public class DoorRowTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShouldThrowArgumentOutOfRangeException_IfDoorCountIsZeroOrNegative(int doorCount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new DoorRow(doorCount));
        }

        [Fact]
        public void ShouldHaveDoorCount_Of100()
        {
            var doorRow = new DoorRow(100);
            Assert.Equal(100, doorRow.DoorCount());
        }

        [Theory]
        [InlineData(5, "#####")]
        [InlineData(1, "#")]
        public void ShouldPrintCorrectRow_AfterDoorRowCreation(int doorCount, string expected)
        {
            var doorRow = new DoorRow(doorCount);
            Assert.Equal(expected, doorRow.ToString());
        }

        [Theory]
        [InlineData(5, "@@@@@")]
        [InlineData(1, "@")]
        public void ShouldPrintCorrectRow_AfterOnePass(int doorCount, string expected)
        {
            var doorRow = new DoorRow(doorCount);
            doorRow.Pass();
            Assert.Equal(expected, doorRow.ToString());
        }

        [Theory]
        [InlineData(5, "@#@#@")]
        [InlineData(1, "@")]
        [InlineData(6, "@#@#@#")]
        public void ShouldPrintCorrectRow_AfterTwoPasses(int doorCount, string expected)
        {
            var doorRow = new DoorRow(doorCount);
            doorRow.Pass();
            doorRow.Pass();
            Assert.Equal(expected, doorRow.ToString());
        }

        [Theory]
        [InlineData(10, "@###@@@###")]
        [InlineData(1, "@")]
        [InlineData(6, "@###@@")]
        public void ShouldPrintCorrectRow_AfterThreePasses(int doorCount, string expected)
        {
            var doorRow = new DoorRow(doorCount);
            doorRow.Pass();
            doorRow.Pass();
            doorRow.Pass();
            Assert.Equal(expected, doorRow.ToString());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Pass_ShouldThrowArgumentOutOfRangeException_WithZeroOrNegativeTimesArgument(int times)
        {
            var doorRow = new DoorRow(5);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => doorRow.Pass(times));
        }

        [Theory]
        [InlineData(10, "@##@####@#")]
        [InlineData(1, "@")]
        [InlineData(6, "@##@##")]
        [InlineData(100, "@##@####@######@########@##########@############@##############@################@##################@")]
        public void Pass_ShouldPrintCorrectRow_AfterHundredPasses(int doorCount, string expected)
        {
            var doorRow = new DoorRow(doorCount);
            doorRow.Pass(100);
            Assert.Equal(expected, doorRow.ToString());
        }
    }
}