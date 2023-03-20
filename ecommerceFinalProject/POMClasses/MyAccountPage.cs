namespace ecommerceFinalProject.POMClasses
{

    internal class MyAccountPage
    {
        private IWebDriver _driver; //Field to hold a webdriver instance

        public MyAccountPage(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators

        public IWebElement DashboardTab => _driver.FindElement(By.LinkText("Dashboard"));
        public IWebElement OrdersTab => _driver.FindElement(By.LinkText("Orders"));
        public IWebElement DownloadsTab => _driver.FindElement(By.LinkText("Downloads"));
        public IWebElement AddressesTab => _driver.FindElement(By.LinkText("Addresses"));
        public IWebElement AccountDetailsTab => _driver.FindElement(By.LinkText("Account details"));
        public IWebElement LogoutTab => _driver.FindElement(By.LinkText("Logout"));
        public IWebElement OrderNumber => _driver.FindElement(By.CssSelector("td[data-title='Order'] > a"));


        public void Logout()
        {
            LogoutTab.Click();
        }

        public void ViewOrders()
        {
            OrdersTab.Click();
        }

        public string GetOrderNumberInHistory()
        {
            return OrderNumber.Text;
        }

    }
}

