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

            return str.TrimEnd();
        }
    }
}