using Moq;
using System;
using System.Linq;
using Xunit;

namespace CharacterCopy.Domain.Tests
{
    public class CopierTests
    {
        private readonly Mock<ISource> _source;
        private readonly Mock<IDestination> _destination;
        private readonly Copier _copier;

        public CopierTests()
        {
            _source = new Mock<ISource>();
            _destination = new Mock<IDestination>();
            _copier = new Copier(_source.Object, _destination.Object);
        }

        [Theory]
        [InlineData('A')]
        [InlineData('B')]
        public void Copy_ShouldReadCharsFromSource_AndWriteThemToDestination_UntilLineEndIsReturned(char expectedChar)
        {
            _source.SetupSequence(x => x.ReadChar())
                .Returns(expectedChar)
                .Returns('\n');

            _copier.Copy();

            _source.Verify(x => x.ReadChar(), Times.Exactly(2));
            _destination.Verify(x => x.WriteChar(expectedChar), Times.Once());
            _destination.Verify(x => x.WriteChar(It.IsAny<char>()), Times.Once());
        }

        [Fact]
        public void Copy_ShouldReadOnlyOneCharFromSource_AndNotWriteNewLine_IfCharIsNewLineChar()
        {
            _source.Setup(x => x.ReadChar())
                .Returns('\n');

            _copier.Copy();

            _source.Verify(x => x.ReadChar(), Times.Once());
            _destination.Verify(x => x.WriteChar(It.IsAny<char>()), Times.Never());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Copy_ShouldThrowArgumentOutOfRangeException_IfCountIsZeroOrNegative(int count)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _copier.Copy(count));
        }

        [Theory]
        [InlineData(new char[] { 'A', 'B' })]
        public void Copy_ShouldReadAndWriteAsManyCharacters_AsIsSpecified(char[] chars)
        {
            _source.Setup(x => x.ReadChars(chars.Length))
                .Returns(chars);

            _copier.Copy(chars.Length);

            _source.Verify(x => x.ReadChars(chars.Length), Times.Once);
            _destination.Verify(x => x.WriteChars(chars), Times.Once);
        }

        [Theory]
        [InlineData(new char[] { 'A', '\n', 'B' }, 1)]
        [InlineData(new char[] { 'A', 'B', '\n', 'A' }, 2)]
        public void Copy_ShouldWriteCharacters_OnlyUntilNewLineCharIsEncountered(char[] chars, int numberOfCharsWritten)
        {
            _source.Setup(x => x.ReadChars(chars.Length))
               .Returns(chars);

            _copier.Copy(chars.Length);

            _source.Verify(x => x.ReadChars(chars.Length), Times.Once);

            var exceptedArray = chars.Take(numberOfCharsWritten).ToArray();

            _destination.Verify(x => x.WriteChars(exceptedArray), Times.Once);
        }
    }
}
