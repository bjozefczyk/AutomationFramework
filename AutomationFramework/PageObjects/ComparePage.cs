using System;
using OpenQA.Selenium;

namespace AutomationFramework.PageObjects
{
    public class ComparePage
    {
        public bool IsOutOfStockDevices
        {
            get
            {
                string status = "Out Of Stock";
                return
                    Browser.Driver.FindElements(
                        By.XPath("//*[@id='content']/table/tbody[1]/tr[6]/td[contains(text(), '" + status + "')]"))
                        .Count > 0;
            }
        }

        public string Price { get; set; }

        public ComparePage RemoveOutOfStockDevices()
        {
            #region complex

/*            ReadOnlyCollection<IWebElement> elements = Browser.Driver.FindElements(By.XPath("//*[@id='content']/table/tbody[1]/tr[6]/td"));  
            for(int i = 1; i <= elements.Count; i++)
            {
                if (elements[i].Text.Equals(status))
                {
                    Browser.Driver.FindElementByXPath("//*[@id='content']/table/tbody[2]/tr/td[" + i + "]/a").Click();
                    break;
                }
            }*/

            #endregion

            Browser.Driver.FindElementByXPath("//*[@id='content']/table/tbody[2]/tr/td[2]/a").Click();
            Browser.WaitForAjax();
            return this;
        }

        public ComparePage AddRandomDeviceToCart()
        {
            Random rnd = new Random();
            int next = rnd.Next(2, 4);
            By locator = By.XPath("//*[@id='content']/table/tbody[2]/tr/td[" + next + "]/input");
            Price =
                Browser.Driver.FindElement(By.XPath("//*[@id='content']/table/tbody[1]/tr[3]/td[" + next + "]")).Text;
            Browser.WaitFor(locator, TimeSpan.FromSeconds(10)).Click();
            Browser.WaitForAjax();
            return this;
        }

        public CartPage OpenCartPage()
        {
            Browser.WaitFor(By.PartialLinkText("Shopping Cart"), TimeSpan.FromSeconds(10)).Click();
            return new CartPage();
        }
    }
}