namespace ecommerceFinalProject.POMClasses
{
    class TopNav
    {
        private IWebDriver _driver;

        public TopNav(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement Shop => _driver.FindElement(By.LinkText("Shop"));
        public IWebElement Cart => _driver.FindElement(By.LinkText("Cart"));
        public IWebElement Checkout => _driver.FindElement(By.LinkText("Checkout"));
        public IWebElement MyAccount => _driver.FindElement(By.LinkText("My account"));
        public IWebElement Home => _driver.FindElement(By.LinkText("Home"));
        public IWebElement Blog => _driver.FindElement(By.LinkText("Blog"));

        

        public void NavigateToShop()
        {
            Shop.Click();
        }

        public void NavigateToCart()
        {
            Cart.Click();
        }

        public void NavigateToCheckout()
        {
            Checkout.Click();
        }

        public void NavigateToMyAccount()
        {
            MyAccount.Click();
        }



    }
}

