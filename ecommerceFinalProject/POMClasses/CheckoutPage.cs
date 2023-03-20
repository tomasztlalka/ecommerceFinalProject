using OpenQA.Selenium.Support.UI;

namespace ecommerceFinalProject.POMClasses
{
    internal class CheckoutPage
    {
        private IWebDriver _driver; //Field to hold a webdriver instance

        public CheckoutPage(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators
        public IWebElement FirstName => _driver.FindElement(By.Id("billing_first_name"));
        public IWebElement LastName => _driver.FindElement(By.Id("billing_last_name"));
        public IWebElement BillingAddress => _driver.FindElement(By.Id("billing_address_1"));
        public IWebElement City => _driver.FindElement(By.Id("billing_city"));
        public IWebElement Postcode => _driver.FindElement(By.Id("billing_postcode"));
        public IWebElement Phone => _driver.FindElement(By.Id("billing_first_name"));
        public IWebElement PlaceOrderButton => _driver.FindElement(By.CssSelector("#place_order"));

        //Service Methods
        public CheckoutPage FillBillingDetails(List<string> billingFields)
        {
            FirstName.Clear();
            LastName.Clear();
            BillingAddress.Clear();
            City.Clear();
            Postcode.Clear();
            Phone.Clear();

            FirstName.SendKeys(billingFields.ElementAt(0));
            LastName.SendKeys(billingFields.ElementAt(1));
            BillingAddress.SendKeys(billingFields.ElementAt(2));
            City.SendKeys(billingFields.ElementAt(3));
            Postcode.SendKeys(billingFields.ElementAt(4));
            Phone.SendKeys(billingFields.ElementAt(5));
       
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.7);

            return this;
        }

        public void SubmitOrder()
        {
            //Workaround the blockUI blockOverlay displayed in Firefox
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div[class='blockUI blockOverlay']")));


            PlaceOrderButton.Click();
            //Wait for order number to appear on page
            WaitForElement(By.CssSelector("li[class='woocommerce-order-overview__order order'] > strong"), 3, _driver);


        }


    }

}