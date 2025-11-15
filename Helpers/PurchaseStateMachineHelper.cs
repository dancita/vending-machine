namespace VendingMachineApp.Helpers
{
    public static class PurchaseStateMachineHelper
    {
        public static readonly Dictionary<string, int> CoinList = new()
        {
            { "3", 5 },
            { "4", 10 },
            { "5", 20 },
            { "6", 50 },
            { "7", 100 },
            { "8", 200 },
        };

        public static readonly Dictionary<string, int> PriceList = new()
        {
            { "9", 100 },
            { "10", 50 },
            { "11", 65 }
        };
    }
}