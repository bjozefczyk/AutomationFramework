using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AutomationFramework.PageObjects
{
    public class CartPage : BasePage
    {

        [FindsBy(How = How.XPath, Using = "//*[@id='content']/div[2]/div/table/tbody/tr[4]/td[2]")]
        private readonly IWebElement _totalPrice;

        public CartPage()
        {
            _url = "http://demo.opencart.com/index.php?route=checkout/cart";
        }

        public string TotalPrice
        {
            get { return _totalPrice.Text; }
        }

        public override void GoTo()
        {
            Browser.GoTo(_url);
        }
    }
}