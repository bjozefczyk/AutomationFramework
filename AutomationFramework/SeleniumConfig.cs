using System.Configuration;

namespace AutomationFramework
{
    public class SeleniumConfig : ConfigurationSection
    {
        private const string Firefox = "FF";
        private const string IExplorer = "IE";
        private const string Chrome = "CH";

        public static SeleniumConfig Current { get; set; }

        static SeleniumConfig()
        {
            Current = ConfigurationManager.GetSection("browserConfig") as SeleniumConfig;
        }

        [ConfigurationProperty("browser")]
        public string Browser
        {
            get { return this["browser"].ToString().ToUpper(); }
        }

        public bool IsChrome
        {
            get { return Chrome.Equals(Browser); }
        }

        public bool IsFirefox
        {
            get { return Firefox.Equals(Browser); }
        }

        public bool IsIExplorer
        {
            get { return IExplorer.Equals(Browser); }
        }
    }
}