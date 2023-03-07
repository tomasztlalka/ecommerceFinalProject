
internal class OrderReceivedPage
{
    private IWebDriver _driver; //Field to hold a webdriver instance
    

    public OrderReceivedPage(IWebDriver driver) //Get the webdriver instance from the calling test
    {
        this._driver = driver;
        
    }

    //Locators
    public IWebElement displayedOrderNumber => _driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"));
}

