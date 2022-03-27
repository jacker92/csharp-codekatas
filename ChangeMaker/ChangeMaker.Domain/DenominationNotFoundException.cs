namespace ChangeMaker.Domain
{
    public class DenominationNotFoundException : Exception
    {
        public DenominationNotFoundException() : base("Denomination cannot be found.")
        {
        }
    }
}