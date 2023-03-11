namespace ecommerceFinalProject.POMClasses
{
    internal class CartPage
    {

        private IWebDriver _driver; //Field to hold a webdriver instance


        public CartPage(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;

        }

        //Locators

        public IWebElement CouponField => _driver.FindElement(By.Name("coupon_code"));

        public IWebElement ApplyButton => _driver.FindElement(By.Name("apply_coupon"));

        public IWebElement ProceedToCheckoutButton => _driver.FindElement(By.LinkText("Proceed to checkout"));

        public IWebElement SiteFooter => _driver.FindElement(By.CssSelector("div[class='col-full']"));

        public IWebElement DeleteButton => _driver.FindElement(By.CssSelector("a[class='remove']"));

        public IWebElement SubTotal => _driver.FindElement(By.CssSelector("td[data-title='Subtotal']"));

        public IWebElement CartTotal => _driver.FindElement(By.CssSelector("td[data-title='Total']"));

        public IWebElement CartEmptyMessage => _driver.FindElement(By.XPath("//*[@id=\"post-5\"]/div/div/p[1]/text()"));

        public IWebElement ReturnToShopButton => _driver.FindElement(By.CssSelector("a[class='button wc-backward']"));

        public IWebElement DismissButton => _driver.FindElement(By.CssSelector("a[class='woocommerce-store-notice__dismiss-link']"));
        

        public string DeleteButtonPath => "a[class='remove']";  //remove
                                                                //Service Methods

        public void EnterCouponCode()
        {
            //Wait for the coupon code text box to appear
            WaitForElement(By.Name("coupon_code"), 2, _driver);
            CouponField.SendKeys(TestContext.Parameters["edgewords_coupon"]);
            ApplyButton.Click();

            //Wait for the coupon to get applied before proceeding further
            WaitForElement(By.CssSelector("tr[class='cart-discount coupon-edgewords']"), 2, _driver);
        }

        public void DeleteItem()
        {
            DeleteButton.Click();
        }

        
    }

}