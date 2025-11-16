namespace VendingMachineApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            string[] invalidOptions = { "1", "2" };
            string[] coinOptions = { "3", "4", "5", "6", "7", "8" };
            string[] buttonOptions = { "9", "10", "11" };
            const int _maxInputValue = 12;

            var purchaseStateMachine = new PurchaseStateMachine();

            while (!endApp)
            {
                Console.WriteLine("===============");
                Console.WriteLine($" {purchaseStateMachine.SetDisplayText()}");
                Console.WriteLine("===============");

                Console.WriteLine("Select an action by pressing a button accordingly: \n");
                Console.WriteLine("---------------------------------------------------\n");
                Console.WriteLine("Inserting 1p: 1");
                Console.WriteLine("Inserting 2p: 2");
                Console.WriteLine("Inserting 5p: 3");
                Console.WriteLine("Inserting 10p: 4");
                Console.WriteLine("Inserting 20p: 5");
                Console.WriteLine("Inserting 50p: 6");
                Console.WriteLine("Inserting £1.00: 7");
                Console.WriteLine("Inserting £2.00: 8\n");
                Console.WriteLine("Selecting cola for £1.00: 9");
                Console.WriteLine("Selecting crisps for 50p: 10");
                Console.WriteLine("Selecting chocolate for 65p: 11");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || !ValidateInput(input))
                {                   
                    Console.WriteLine("Input was INVALID! Try again...");
                }
                else
                {
                    ValidateSelectedOption(input);

                    CheckIfCoinInsertion(input);

                    if (CheckIfButtonPress(input))
                    {
                        Console.WriteLine($"{purchaseStateMachine.CurrentDisplay}");
                        if (purchaseStateMachine.CoinsToReturn != 0)
                        {
                            Console.WriteLine($"(coins are placed in the coin return: {purchaseStateMachine.CoinsToReturn}p) \n");
                        }
                    }
                }       
                
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }

            void CheckIfCoinInsertion(string selectedOption)
            {
                if (coinOptions.Contains(selectedOption))
                {
                    purchaseStateMachine.AddCoins(selectedOption);
                }
            }

            void ValidateSelectedOption(string selectedOption)
            {
                if (invalidOptions.Contains(selectedOption))
                {
                    Console.WriteLine("(coin has been placed in the coin return)\n");
                }
            }

            bool CheckIfButtonPress(string selectedOption)
            {
                if (buttonOptions.Contains(selectedOption))
                {
                    purchaseStateMachine.PressButton(selectedOption);
                    return true;
                }

                return false;

            }

            bool ValidateInput(string input)
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
}