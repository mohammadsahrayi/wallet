namespace Transaction.Framework.Types
{
    using System.ComponentModel;


    public enum Currency
    {
        Unknown = 0,

        [Description("Rial Iran")]
        Rial = 1,

        [Description("United States dollar")]
        USD = 2
    }
    public static class CurrencyType
    {
        public const string Rial = "Rial";
        public const string USD = "USD";
    }


}
