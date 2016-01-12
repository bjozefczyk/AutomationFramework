using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AutomationFramework.PageObjects
{
    public class MainPage : BasePage
    {

        [FindsBy(How = How.XPath, Using = "//*[@id='currency']/div/button")] 
        private readonly IWebElement _currencyButton;

        public IWebElement CurrencyButton
        {
            get { return _currencyButton; }
        }

        [FindsBy(How = How.Name, Using = "GBP")] 
        private readonly IWebElement _currencyGBP;

        public IWebElement CurrencyGBP
        {
            get { return _currencyGBP; }
        }

        [FindsBy(How = How.Name, Using = "search")] 
        private readonly IWebElement _searchInput;

        public IWebElement searchInput
        {
            get { return _searchInput; }
        }

        public MainPage()
        {
            _url = "http://demo.opencart.com";
        }

        public override void GoTo()
        {
            Browser.Driver.Manage().Window.Maximize();
            Browser.GoTo(_url);
        }

        public MainPage ChangeCurrencyToGBP()
        {
            CurrencyButton.Click();
            CurrencyGBP.Click();
            return this;
        }

        public MainPage SearchText(string text)
        {
            searchInput.SendKeys(text);
            searchInput.SendKeys(Keys.Enter);
            return this;
        }

        public string ActualCurrency
        {
            get { return Browser.Driver.FindElement(By.XPath("//*[@id='currency']/div/button")).Text.Substring(0, 1); }
        }

        public MainPage AddItemsToComparitionByText(string title)
        {
            var iPods =
                Browser.Driver.FindElements(
                    By.CssSelector("div[class*='button-group'] button[data-original-title='" + title + "']"));
            if (!SeleniumConfig.Current.IsIExplorer)
            {
                foreach (var iPod in iPods)
                {
                    Browser.Driver.ExecuteScript("arguments[0].scrollIntoView(true);", iPod);
                    iPod.Click();
                }
            }
            else
            {
                By locator;
                IWebElement compareButton;
                for (int i = 1; i <= iPods.Count; i++)
                {
                    locator = By.XPath("//*[@id='content']/div[4]/div[" + i + "]/div/div[3]/button[3]");
                    compareButton = Browser.WaitFor(locator, TimeSpan.FromSeconds(10));
                    Browser.Driver.ExecuteScript("arguments[0].scrollIntoView(true);", compareButton);
                    compareButton.Click();
                    Browser.WaitFor(By.PartialLinkText("product comparison"), TimeSpan.FromSeconds(10));
                }
            }

            return this;
        }

        public ComparePage OpenComparePage()
        {
            Browser.WaitFor(By.PartialLinkText("Product Compare (4)"), TimeSpan.FromSeconds(15)).Click();
            return new ComparePage();
        }
    }
}