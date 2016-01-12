namespace AutomationFramework.PageObjects
{
    public static class Pages
    {
        public static MainPage MainPage
        {
            get { return new MainPage(); }
        }

        public static CartPage CartPage
        {
            get { return new CartPage(); }
        }

        public static ComparePage ComparePage
        {
            get { return new ComparePage(); }
        }
    }
}