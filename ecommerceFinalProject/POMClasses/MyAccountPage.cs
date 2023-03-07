internal class MyAccountPage
{
    private IWebDriver _driver; //Field to hold a webdriver instance

    public MyAccountPage(IWebDriver driver) //Get the webdriver instance from the calling test
    {
        this._driver = driver;
    }

    //Locators

    public IWebElement dashboardButton => _driver.FindElement(By.Id("***"));
    public IWebElement ordersButton => _driver.FindElement(By.CssSelector("#post-7 > div > div > nav > ul > li.woocommerce-MyAccount-navigation-link.woocommerce-MyAccount-navigation-link--orders > a"));
    public IWebElement downloadsButton => _driver.FindElement(By.Id("***"));
    public IWebElement addressesButton => _driver.FindElement(By.Id("***"));
    public IWebElement accountDetailsButton => _driver.FindElement(By.Id("***"));
    public IWebElement logoutButton => _driver.FindElement(By.LinkText("Logout"));


    public IWebElement orderNumber => _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));
}

