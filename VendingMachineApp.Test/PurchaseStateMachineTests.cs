using VendingMachineApp.Enums;

namespace VendingMachineApp.Test
{
    [TestClass]
    public class PurchaseStateMachineTests
    {
        [TestMethod]
        public void ProcessTransitionIsSuccessfulWhenTransitionIsAllowed()
        {
            var machine = new PurchaseStateMachine();

            machine.ProcessTransition(VendingMachineTrigger.PressButton);

            Assert.AreEqual(VendingMachineState.ButtonIsPressed, machine.CurrentState);
        }

        [TestMethod]
        public void ProcessTransitionThrowsErrorWhenTransitionIsNotAllowed()
        {
            var machine = new PurchaseStateMachine();

            void act() => machine.ProcessTransition(VendingMachineTrigger.AttemptWithEnoughFunds);
            var exception = Assert.ThrowsException<Exception>(act);

            Assert.IsNotNull(exception);
            Assert.AreEqual("Invalid transition: WaitingForPayment cannot handle AttemptWithEnoughFunds", exception.Message);
        }

        [TestMethod]
        public void PressButtonDisplaysPriceIfAmountIsNotEnough()
        {
            var machine = new PurchaseStateMachine();

            machine.PressButton("9");

            Assert.AreEqual(VendingMachineState.WaitingForPayment, machine.CurrentState);
            Assert.AreEqual("PRICE £1.00", machine.CurrentDisplay);
        }

        [TestMethod]
        public void PressButtonDispensesProductWithNoChangeIfAmountIsSameAsPrice()
        {
            var machine = new PurchaseStateMachine();
            machine.AddCoins("7"); //adds £1

            machine.PressButton("9"); //costs £1

            Assert.AreEqual(VendingMachineState.WaitingForPayment, machine.CurrentState);
            Assert.AreEqual("THANK YOU", machine.CurrentDisplay);
            Assert.AreEqual(0, machine.CoinsToReturn);
            Assert.AreEqual(0, machine.CurrentAmount);
        }

        [TestMethod]
        [DataRow("7", "10", 50)]
        [DataRow("7", "11", 35)]
        [DataRow("8", "11", 135)]
        public void PressButtonDispensesProductWithChangeIfAmountIsHigherThanPrice(string coin, string product, int expectedChange)
        {
            var machine = new PurchaseStateMachine();
            machine.AddCoins(coin);

            machine.PressButton(product);

            Assert.AreEqual(VendingMachineState.WaitingForPayment, machine.CurrentState);
            Assert.AreEqual("THANK YOU", machine.CurrentDisplay);
            Assert.AreEqual(expectedChange, machine.CoinsToReturn);
            Assert.AreEqual(0, machine.CurrentAmount);
        }

        [TestMethod]
        [DataRow("INSERT COIN", false)]
        [DataRow("AMOUNT £0.05", true, "3")]
        [DataRow("AMOUNT £0.20", true, "5")]
        [DataRow("AMOUNT £2.00", true, "8")]
        public void SetDisplayTextShowsFormattedText(string displayText, bool addCoin, string coinCode = "")
        {
            var machine = new PurchaseStateMachine();
            if (addCoin && !String.IsNullOrEmpty(coinCode))
            {
                machine.AddCoins(coinCode);
            }

            machine.SetDisplayText();

            Assert.AreEqual(displayText, machine.CurrentDisplay);
        }

        [TestMethod]
        [DataRow("AMOUNT £1.10", "4", "7")]
        [DataRow("AMOUNT £0.10", "3", "3")]
        [DataRow("AMOUNT £3.00", "8", "7")]
        [DataRow("AMOUNT £0.15", "3", "4")]
        public void SetDisplayTextShowsFormattedTextAfterAddingMultipleCoins(string displayText, string firstCoin, string secondCoin)
        {
            var machine = new PurchaseStateMachine();

            machine.AddCoins(firstCoin);
            machine.AddCoins(secondCoin);

            machine.SetDisplayText();

            Assert.AreEqual(displayText, machine.CurrentDisplay);
        }
    }
}