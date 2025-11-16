using VendingMachineApp;
using VendingMachineApp.Helpers;

bool endApp = false;

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

    if (!InputHelper.IsInputValid(input))
    {                   
        Console.WriteLine("Input was INVALID! Try again...");
    }
    else if (InputHelper.IsInvalidCoin(input))
    {
        Console.WriteLine("(coin has been placed in the coin return)\n");
    }
    else
    {
        if (InputHelper.IsCoinInsertion(input))
        {
            purchaseStateMachine.AddCoins(input);
        }

        else if (InputHelper.IsButtonPress(input))
        {
            purchaseStateMachine.PressButton(input);
            Console.WriteLine($"{purchaseStateMachine.CurrentDisplay}");
            if (purchaseStateMachine.CoinsToReturn != 0)
            {
                Console.WriteLine($"(coins are placed in the coin return: {purchaseStateMachine.CoinsToReturn}p) \n");
                purchaseStateMachine.TakeCoinsFromReturn();
            }
        }
    }       
                
    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n") endApp = true;

    Console.WriteLine("\n");
}