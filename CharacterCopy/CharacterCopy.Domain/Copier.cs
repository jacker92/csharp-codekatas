using System;
using System.Linq;

namespace CharacterCopy.Domain
{
    public class Copier
    {
        private readonly ISource _source;
        private readonly IDestination _destination;

        public Copier(ISource source, IDestination destination)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

        public void Copy()
        {
            while (true)
            {
                var c = _source.ReadChar();

                if (c == '\n')
                {
                    return;
                }

                _destination.WriteChar(c);
            }
        }

        public void Copy(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            var chars = _source.ReadChars(count);

            var newLineCharIndex = Array.IndexOf(chars, '\n');

            var filteredChars = newLineCharIndex == -1 ?
                chars :
                chars.Take(newLineCharIndex).ToArray();

            _destination.WriteChars(filteredChars);
        }
    }
}
