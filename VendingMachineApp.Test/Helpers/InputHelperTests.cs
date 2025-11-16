using VendingMachineApp.Helpers;

namespace VendingMachineApp.Test.Helpers
{
    [TestClass]
    public class InputHelperTests
    {
        [TestMethod]
        public void IsValidReturnsFalseWhenInputIsEmpty()
        {
            var result = InputHelper.IsInputValid("");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidReturnsFalseWhenInputIsNull()
        {
            var result = InputHelper.IsInputValid("");

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("0")]
        [DataRow("12")]
        [DataRow("13")]
        [DataRow("7777")]
        public void IsValidReturnsFalseWhenInputIsNotInSpecifiedRange(string input)
        {
            var result = InputHelper.IsInputValid(input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("a")]
        [DataRow("bbbb")]
        [DataRow("%")]
        [DataRow("***")]
        public void IsValidReturnsFalseWhenInputIsNotInt(string input)
        {
            var result = InputHelper.IsInputValid(input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("5")]
        [DataRow("9")]
        [DataRow("11")]
        public void IsValidReturnsTrueWhenInputIsValid(string input)
        {
            var result = InputHelper.IsInputValid(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("2")]
        public void IsCoinValidReturnsFalseWhenInputIsNotValidCoinCode(string input)
        {
            var result = InputHelper.IsInvalidCoin(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("3")]
        [DataRow("5")]
        [DataRow("7")]
        public void IsCoinValidReturnsTrueWhenInputIsValidCoinCode(string input)
        {
            var result = InputHelper.IsInvalidCoin(input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("3")]
        [DataRow("5")]
        [DataRow("8")]
        public void IsCoinInsertionReturnsTrueWhenInputIsCoinInsertion(string input)
        {
            var result = InputHelper.IsCoinInsertion(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("9")]
        [DataRow("10")]
        [DataRow("10000")]
        public void IsCoinInsertionReturnsFalseWhenInputIsNotCoinInsertion(string input)
        {
            var result = InputHelper.IsCoinInsertion(input);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("9")]
        [DataRow("10")]
        [DataRow("11")]
        public void IsButtonPressReturnsTrueWhenInputIsButtonPress(string input)
        {
            var result = InputHelper.IsButtonPress(input);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("8")]
        [DataRow("20")]
        [DataRow("10000")]
        public void IsButtonPressReturnsFalseWhenInputIsNotButtonPress(string input)
        {
            var result = InputHelper.IsButtonPress(input);

            Assert.IsFalse(result);
        }
    }
}