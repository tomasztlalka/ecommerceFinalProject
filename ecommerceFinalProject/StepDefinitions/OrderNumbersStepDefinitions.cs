namespace ecommerceFinalProject.StepDefinitions
{
    [Binding]
    public class OrderNumbersStepDefinitions
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        
        public OrderNumbersStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["driver"];
        }
        

        [When(@"I successfully complete checkout using these details")]
        public void WhenISuccessfullyCompleteCheckoutUsingTheseDetails(Table table)
        {
            ShopPage shopPage = new ShopPage(_driver);
            shopPage.ViewCart();

            CartPage cartPage = new CartPage(_driver);
            cartPage.ProceedToCheckout();

            List<string> billingDetails = new List<string>();
            foreach (TableRow row in table.Rows)
            {
                billingDetails.Add(row["first_name"]);
                billingDetails.Add(row["last_name"]);
                billingDetails.Add(row["address_line1"]);
                billingDetails.Add(row["city"]);
                billingDetails.Add(row["postcode"]);
                billingDetails.Add(row["phone"]);
            }

            CheckoutPage checkoutPage = new CheckoutPage(_driver);
            checkoutPage.FillBillingDetails(billingDetails);
            checkoutPage.SubmitOrder();
        }




        [Then(@"order number shown after checkout matches the one in Orders page")]
        public void ThenOrderNumberShownAfterCheckoutMatchesTheOneInOrdersPage()
        {
            OrderReceivedPage orderReceivedPage = new OrderReceivedPage(_driver);
            //Capture the initial order number
            string orderNumberAtCheckout = orderReceivedPage.GetOrderNumberAtCheckout();
            //Take a screenshot of the initial order number displayed right after the checkout page
            TakeScreenshotOfElement(_driver, "div[class='woocommerce-order']", "test2_initialorder");
            //Write the initial order number to console
            Console.WriteLine("Order number at checkout is " + orderNumberAtCheckout);

            TopNav topNav = new TopNav(_driver);
            topNav.NavigateToMyAccount();

            MyAccountPage myAccountPage = new MyAccountPage(_driver);
            //Navigate to 'Orders' tab on 'My account' page
            myAccountPage.ViewOrders();
            //Capture the order number present in order history
            string orderNumberFromHistory = myAccountPage.GetOrderNumberInHistory();
            //Take a screenshot of the order number present in the 'Orders' tab
            TakeScreenshotOfElement(_driver, "tbody >tr", "test2_secondorder");
            //Write the order number from 'Orders' tab to console
            Console.WriteLine("Order number from 'Orders' is " + orderNumberFromHistory);

            Assert.That(orderNumberAtCheckout, Is.EqualTo(orderNumberFromHistory), "Order number does not match");
        }
    }
}
