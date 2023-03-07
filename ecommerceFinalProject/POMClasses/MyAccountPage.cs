internal class MyAccountPage
{
    private IWebDriver _driver; //Field to hold a webdriver instance

    public MyAccountPage(IWebDriver driver) //Get the webdriver instance from the calling test
    {
        this._driver = driver;
    }

    //Locators

    public IWebElement dashboardTab => _driver.FindElement(By.LinkText("Dashboard"));
    public IWebElement ordersTab => _driver.FindElement(By.LinkText("Orders"));
    public IWebElement downloadsTab => _driver.FindElement(By.LinkText("Downloads"));
    public IWebElement addressesTab => _driver.FindElement(By.LinkText("Addresses"));
    public IWebElement accountDetailsTab => _driver.FindElement(By.LinkText("Account details"));
    public IWebElement logoutTab => _driver.FindElement(By.LinkText("Logout"));


    public IWebElement orderNumber => _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));
}

