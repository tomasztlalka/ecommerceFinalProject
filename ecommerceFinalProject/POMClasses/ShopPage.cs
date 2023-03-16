namespace ecommerceFinalProject.POMClasses
{

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

        public void AddItemToCart()
        {
            _topNav.Shop.Click();
            string Item = TestContext.Parameters["item"];

            //if (Item == "random" || Item == null)
            //{
            //    Item = "";
            //}
            //TODO: write a "random" item case too

            _driver.FindElement(By.CssSelector("a[aria-label='Add “" + Item + "” to your cart']")).Click();
            
        }

        public void ViewCart()
        {
            WaitForElement(By.LinkText("View cart"), 2, _driver);
            _topNav.Cart.Click();
        }


    }

}