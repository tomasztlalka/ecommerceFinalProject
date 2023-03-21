using OpenQA.Selenium.Support.UI;

namespace ecommerceFinalProject.POMClasses
{
    internal class CartPage
    {

        private IWebDriver _driver; //Field to hold a webdriver instance

        decimal expectedTotal;
        decimal actualTotal;

        public CartPage(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;

        }

        //Locators
        public IWebElement CouponField => _driver.FindElement(By.Name("coupon_code"));

        public IWebElement ApplyButton => _driver.FindElement(By.Name("apply_coupon"));

        public IWebElement ProceedToCheckoutButton => _driver.FindElement(By.LinkText("Proceed to checkout"));

        public IWebElement SiteFooter => _driver.FindElement(By.CssSelector("#colophon > div > div.site-info"));

        public IWebElement DeleteButton => _driver.FindElement(By.CssSelector("a[class='remove']"));

        public IWebElement SubTotal => _driver.FindElement(By.CssSelector("td[data-title='Subtotal']"));

        public IWebElement CartTotal => _driver.FindElement(By.CssSelector("td[data-title='Total']"));

        //public IWebElement CartEmptyMessage => _driver.FindElement(By.XPath("//*[@id=\"post-5\"]/div/div/p[1]/text()"));

        public IWebElement ReturnToShopButton => _driver.FindElement(By.CssSelector("a[class='button wc-backward']"));

        public IWebElement DismissButton => _driver.FindElement(By.CssSelector("a[class='woocommerce-store-notice__dismiss-link']"));

        
        //Service Methods

        public void EnterCouponCode(string couponCode)
        {
            CheckNull(couponCode);
            //Wait for the coupon code text box to appear
            WaitForElement(By.Name("coupon_code"), 2, _driver);
            CouponField.SendKeys(couponCode);
            ApplyButton.Click();

            //Wait for the coupon to get applied before proceeding further
            WaitForElement(By.CssSelector("tr[class='cart-discount coupon-edgewords']"), 2, _driver);
        }


        //Method for navigating to the Cart and removing any item that is in it
        public void ClearCart()
        {
            TopNav topNav = new TopNav(_driver);
            CartPage cartPage = new CartPage(_driver);
            topNav.NavigateToCart();

            try
            {
                while (true)
                {
                    cartPage.DeleteItem();
                    //WaitForElement(By.CssSelector("a[class='remove']"), 3, _driver);
                    Thread.Sleep(1000);
                }
            }
            catch
            {
                ReturnToShopButton.Click();
            }
        }
      
        public decimal CalculateExpectedTotal(decimal discPercentage)
        {
            decimal subTotal = decimal.Parse(SubTotal.Text[1..]);
            decimal shippingFee = decimal.Parse(GetContextParameter("shipping_fee"));
            expectedTotal = (subTotal * (1 - discPercentage / 100)) + shippingFee;
            return expectedTotal;
        }

        public decimal GetActualTotal()
        {
            actualTotal = decimal.Parse(CartTotal.Text[1..]);
            return actualTotal;
        }


        public void DismissNoticeBar()
        {
            DismissButton.Click();
        }


        public void ProceedToCheckout()
        {
            ProceedToCheckoutButton.Click();
        }

        public void DeleteItem()
        {
            DeleteButton.Click();
        }



    }

}