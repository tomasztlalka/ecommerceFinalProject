namespace ecommerceFinalProject.POMClasses
{
    internal class OrderReceivedPage
    {
        private IWebDriver _driver; //Field to hold a webdriver instance


        public OrderReceivedPage(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;

        }

        //Locators
        public IWebElement DisplayedOrderNumber => _driver.FindElement(By.CssSelector("li[class='woocommerce-order-overview__order order'] > strong"));
    

        public string GetDisplayedOrderNumber()
        {
            return DisplayedOrderNumber.Text;
        }
    
    }

}