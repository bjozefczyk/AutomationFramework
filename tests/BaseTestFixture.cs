using AutomationFramework;
using NUnit.Framework;

namespace Tests
{
    public class BaseTestFixture
    {
        [OneTimeSetUp]
        public virtual void SetupFixture()
        {
        }

        [SetUp]
        protected virtual void SetUp()
        {
            if (Browser.Driver == null)
                Browser.CreateDriver();
        }

        [TearDown]
        protected void TearDown()
        {
            Browser.Quit();
        }

        [OneTimeTearDown]
        protected void TestFixtureTearDown()
        {
            Browser.Quit();
        }
    }
}