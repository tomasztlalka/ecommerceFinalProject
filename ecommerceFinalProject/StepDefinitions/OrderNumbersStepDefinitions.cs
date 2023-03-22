namespace ecommerceFinalProject.StepDefinitions
{
    [Binding]
    public class OrderNumbersStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ShopPage _shopPage;
        private readonly CheckoutPage _checkoutPage;
        private readonly CartPage _cartPage;
        private readonly TopNav _topNav;
        private readonly MyAccountPage _myAccountPage;
        private readonly OrderReceivedPage _orderReceivedPage;
        public OrderNumbersStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _shopPage = (ShopPage)_scenarioContext["shopPage"];
            _checkoutPage= (CheckoutPage)_scenarioContext["checkoutPage"];
            _cartPage = (CartPage)_scenarioContext["cartPage"];
            _topNav = (TopNav)_scenarioContext["topNav"];
            _myAccountPage = (MyAccountPage)_scenarioContext["myAccountPage"];
            _orderReceivedPage = (OrderReceivedPage)_scenarioContext["orderReceivedPage"];
        }
        

        [When(@"I successfully complete checkout using these details")]
        public void WhenISuccessfullyCompleteCheckoutUsingTheseDetails(Table table)
        {
            _shopPage.ViewCart();
            _cartPage.ProceedToCheckout();

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
            _checkoutPage.FillBillingDetails(billingDetails);
            _checkoutPage.SubmitOrder();
        }




        [Then(@"order number shown after checkout matches the one in Orders page")]
        public void ThenOrderNumberShownAfterCheckoutMatchesTheOneInOrdersPage()
        {
            //Capture the initial order number
            string orderNumberAtCheckout = _orderReceivedPage.GetOrderNumberAtCheckout();
            //Take a screenshot of the initial order number displayed right after the checkout page
            TakeScreenshotOfElement("div[class='woocommerce-order']", "test2_initialorder");
            //Write the initial order number to console
            Console.WriteLine("Order number at checkout is " + orderNumberAtCheckout);

            _topNav.NavigateToMyAccount();
            //Navigate to 'Orders' tab on 'My account' page
            _myAccountPage.ViewOrders();

            //Capture the order number present in order history
            string orderNumberFromHistory = _myAccountPage.GetOrderNumberInHistory();
            //Take a screenshot of the order number present in the 'Orders' tab
            TakeScreenshotOfElement("tbody >tr", "test2_secondorder");
            //Write the order number from 'Orders' tab to console
            Console.WriteLine("Order number from 'Orders' is " + orderNumberFromHistory);

            Assert.That(orderNumberAtCheckout, Is.EqualTo(orderNumberFromHistory), "Order number does not match");
        }
    }
}
