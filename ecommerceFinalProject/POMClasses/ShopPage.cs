

internal class ShopPage
{

    private IWebDriver _driver; //Field to hold a webdriver instance
    private TopNav _topNav;

    public ShopPage(IWebDriver driver, TopNav topNav) //Get the webdriver instance from the calling test
    {
        this._driver = driver;
        this._topNav = topNav;
    }

        
    //Service Methods

    public void AddItemToCart(string itemPathInCSS)
    {
        _topNav.Shop.Click();
        //TODO: Change XPath to Css
        _driver.FindElement(By.XPath(itemPathInCSS)).Click();
    }

    public void ViewCart()
    {
        WaitForElement(By.LinkText("View cart"), 2, _driver);
        _topNav.Cart.Click();
    }


}

