namespace VendingMachineApp.Helpers
{
    public static class InputHelper
    {
        const int _maxInputValue = 12;

        public static readonly string[] InvalidCoinCodes = { "1", "2" };

        public static readonly string[] coinOptions = { "3", "4", "5", "6", "7", "8" };

        public static readonly string[] buttonOptions = { "9", "10", "11" };

        public static bool IsInputValid(string? input)
        {
            if (string.IsNullOrEmpty(input) || !ValidateInput(input))
            {
                return false;
            }

            return true;
        }

        public static bool IsInvalidCoin(string input)
        {
            if (InvalidCoinCodes.Contains(input))
            {
                return true;
            }

            return false;
        }

        public static bool IsCoinInsertion (string input)
        {
            if (coinOptions.Contains(input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsButtonPress(string input)
        {
            if (buttonOptions.Contains(input))
            {
                return true;
            }

            return false;
        }

        private static bool ValidateInput(string input)
        {
            bool isInt = int.TryParse(input, out int value);

            if (isInt && value < _maxInputValue && value > 0)
            {
                return true;
            }

            return false;
        }
    }
}