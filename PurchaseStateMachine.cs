using VendingMachineApp.Enums;
using VendingMachineApp.Helpers;

namespace VendingMachineApp
{
    public class PurchaseStateMachine
    {
        private readonly Dictionary<(VendingMachineState, VendingMachineTrigger), VendingMachineState> stateTransitions;

        public VendingMachineState CurrentState { get; private set; }

        public decimal CurrentAmount { get; private set; }

        public string CurrentDisplay { get; private set; }

        public decimal CoinsToReturn { get; private set; }

        private readonly string _purchaseStartedText = "INSERT COIN";
        private readonly string _purchaseFinishedText = "THANK YOU";
        private readonly string _priceDisplayText = "PRICE";
        private readonly string _amountDisplayText = "AMOUNT";

        public PurchaseStateMachine()
        {
            CurrentState = VendingMachineState.WaitingForPayment;
            CurrentDisplay = _purchaseStartedText;
            stateTransitions = new Dictionary<(VendingMachineState, VendingMachineTrigger), VendingMachineState>
            {
                { (VendingMachineState.WaitingForPayment, VendingMachineTrigger.PressButton), VendingMachineState.ButtonIsPressed },
                { (VendingMachineState.ButtonIsPressed, VendingMachineTrigger.AttemptWithInsufficientFunds), VendingMachineState.WaitingForPayment },
                { (VendingMachineState.ButtonIsPressed, VendingMachineTrigger.AttemptWithEnoughFunds), VendingMachineState.WaitingForPayment },             
            };
        }

        public void ProcessTransition(VendingMachineTrigger trigger)
        {
            if (stateTransitions.TryGetValue((CurrentState, trigger), out VendingMachineState newState)) {
                CurrentState = newState;
            }
            else
            {
                throw new Exception($"Invalid transition: {CurrentState} cannot handle {trigger}");
            }
        }

        public void PressButton(string productCode)
        {
            ProcessTransition(VendingMachineTrigger.PressButton);
            int productPrice = GetValueByKey(productCode, PurchaseStateMachineHelper.PriceList);

            if (CurrentAmount < productPrice)
            {
                CurrentDisplay = $"{_priceDisplayText} {ConvertAmountToPounds(productPrice)}";
                ProcessTransition(VendingMachineTrigger.AttemptWithInsufficientFunds);
            }
            else
            {
                CurrentDisplay = _purchaseFinishedText;
                if (CurrentAmount != productPrice)
                {
                    CoinsToReturn = CurrentAmount - productPrice;
                }
                
                ResetCurrentAmount();
                ProcessTransition(VendingMachineTrigger.AttemptWithEnoughFunds);
            }
        }

        private static int GetValueByKey(string key, Dictionary<string, int> dictionary)
        {
            return dictionary[key];
        }

        private void ResetCurrentAmount()
        {
            CurrentAmount = 0;
        }

        public void AddCoins(string coinKey)
        {
            int coinValue = GetValueByKey(coinKey, PurchaseStateMachineHelper.CoinList);

            CurrentAmount += coinValue;

            SetDisplayText();
        }

        public string SetDisplayText()
        {
            if (CurrentAmount == 0)
            {
                CurrentDisplay = _purchaseStartedText;
            }
            else
            {
                CurrentDisplay = $"{_amountDisplayText} {ConvertAmountToPounds(CurrentAmount)}";
            }
            return CurrentDisplay;
        }

        private static string ConvertAmountToPounds(decimal currentAmount)
        {
            var helper = currentAmount / 100m;
            return helper.ToString("C");
        }
    }
}