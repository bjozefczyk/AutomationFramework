using AutomationFramework.PageObjects;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class OpenCartTestFixture : BaseTestFixture
    {
        [SetUp]
        protected void SetUpTest()
        {
            Pages.MainPage.GoTo();
            Assert.IsTrue(Pages.MainPage.IsAt(), "Main page isn't loaded");
        }


        [Test]
        public void TestScenario()
        {
            var deviceName = "IPod";

            Pages.MainPage.ChangeCurrencyToGBP();
            string currency = Pages.MainPage.ActualCurrency;
            Assert.AreEqual(currency, "\u00A3", "Currency should be " + currency + ", but it is not.");
            Pages.MainPage.SearchText(deviceName).AddItemsToComparitionByText("Compare this Product").OpenComparePage();
            Assert.IsTrue(Pages.ComparePage.IsOutOfStockDevices, "No out of stock devices, but should be");
            Pages.ComparePage.RemoveOutOfStockDevices();
            Assert.IsFalse(Pages.ComparePage.IsOutOfStockDevices,
                "There are out of stock devices, but should be removed");
            var productPrice = Pages.ComparePage.AddRandomDeviceToCart().Price;
            Pages.CartPage.GoTo();
            var totalPrice = Pages.CartPage.TotalPrice;
            Assert.IsTrue(totalPrice.Equals(productPrice), "Total price should equal" + productPrice + ", but it is not");
        }
    }
}