using OpenQA.Selenium.Support.PageObjects;

namespace AutomationFramework.PageObjects
{
    public abstract class BasePage
    {
        private const string PageTitle = "Your Store";

        protected string _url;

        public BasePage()
        {
            PageFactory.InitElements(Browser.Driver, this);
        }

        public virtual bool IsAt()
        {
            return Browser.Driver.Title.Contains(PageTitle);
        }

        public abstract void GoTo();
    }
}