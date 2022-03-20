using System.Text;

namespace EndOfLineWhitespaceTrimming.Domain
{
    public class WhitespaceTrimmer
    {
        public string Trim(string str)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            var indexThatNeedToBeRemoved = GetRemovableWhitespaceIndexes(str);

            return TrimString(str, indexThatNeedToBeRemoved);
        }

        private static string TrimString(string str, List<int> indexThatNeedToBeRemoved)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if (indexThatNeedToBeRemoved.Contains(i))
                {
                    continue;
                }

                builder.Append(str[i]);
            }

            return builder.ToString();
        }

        private static List<int> GetRemovableWhitespaceIndexes(string str)
        {
            var indexThatNeedToBeRemoved = new List<int>();

            AddEndOfLineWhitespaceIndexes(str, indexThatNeedToBeRemoved);
            AddEndOfStringWhitespaceIndexes(str, indexThatNeedToBeRemoved);

            return indexThatNeedToBeRemoved;
        }

        private static void AddEndOfLineWhitespaceIndexes(string str, List<int> indexThatNeedToBeRemoved)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str.AllIndexOf("\r\n", StringComparison.InvariantCultureIgnoreCase).Any(x => x == i) ||
                    str.AllIndexOf("\n", StringComparison.InvariantCultureIgnoreCase).Any(x => x == i))
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (str[j] == ' ')
                        {
                            indexThatNeedToBeRemoved.Add(j);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if (str.AllIndexOf("\t", StringComparison.InvariantCultureIgnoreCase).Any(x => x == i))
                {
                    indexThatNeedToBeRemoved.Add(i);
                    indexThatNeedToBeRemoved.Add(i + 1);
                }
            }
        }

        private static void AddEndOfStringWhitespaceIndexes(string str, List<int> indexThatNeedToBeRemoved)
        {
            for (int j = str.Length - 1; j >= 0; j--)
            {
                if (j < 0)
                {
                    break;
                }

                if (str[j] == ' ')
                {
                    indexThatNeedToBeRemoved.Add(j);
                }
                else
                {
                    break;
                }
            }
        }
    }
}