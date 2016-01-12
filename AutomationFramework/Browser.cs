using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework
{
    public static class Browser
    {
        private static RemoteWebDriver _driver;
        private static Uri _uri = new Uri("http://localhost:4444/wd/hub");
        private static readonly TimeSpan DefaultImplicitlyWait = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan DefaultScriptTimeout = TimeSpan.FromSeconds(3);

        private static readonly Type[] DefaultWaitForElementIgnoredExceptions =
        {
            typeof (StaleElementReferenceException),
            typeof (InvalidOperationException),
            typeof (NoSuchElementException),
            typeof (InvalidSelectorException)
        };

        public static RemoteWebDriver Driver
        {
            get { return _driver; }
        }

        public static void CreateDriver()
        {
            _driver = new RemoteWebDriver(_uri, CreateDesiredCapabilities());
            _driver.Manage().Timeouts().ImplicitlyWait(DefaultImplicitlyWait);
            _driver.Manage().Timeouts().SetScriptTimeout(DefaultScriptTimeout);
        }

        public static void GoTo(string url)
        {
            _driver.Url = url;
        }

        public static IWebElement WaitFor(By by, TimeSpan timeout)
        {
            var timeouts = Driver.Manage().Timeouts();
            timeouts.ImplicitlyWait(TimeSpan.MinValue);
            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(Driver, timeout);
                wait.IgnoreExceptionTypes(DefaultWaitForElementIgnoredExceptions);
                return wait.Until(drv => drv.FindElement(by));
            }
            finally
            {
                timeouts.ImplicitlyWait(DefaultImplicitlyWait);
            }
        }

        public static void WaitForAjax()
        {
            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            wait.IgnoreExceptionTypes(DefaultWaitForElementIgnoredExceptions);
            wait.Until(d => (bool) (d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }

        public static void Close()
        {
            if (_driver != null)
                _driver.Close();
        }

        public static void Quit()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                }
                finally
                {
                    _driver = null;
                }
            }
        }

        private static DesiredCapabilities CreateDesiredCapabilities()
        {
            DesiredCapabilities capabilities;

            switch (SeleniumConfig.Current.Browser)
            {
                case "FF":
                    capabilities = DesiredCapabilities.Firefox();
                    capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
                    break;

                case "IE":
                    capabilities = DesiredCapabilities.InternetExplorer();
                    capabilities.SetCapability("enablePersistentHover", false);
                    capabilities.SetCapability("ie.ensureCleanSession", true);
                    capabilities.SetCapability(CapabilityType.BrowserName, "internet explorer");
                    break;

                default:
                    capabilities = DesiredCapabilities.Chrome();
                    capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
                    break;
            }
            return capabilities;
        }
    }
}