using static ecommerceFinalProject.Utils.TestBaseSpecflow;

namespace ecommerceFinalProject.StepDefinitions
{
    [Binding]
    public class OrderNumbersStepDefinitions
    {
        //Initialise instances of POM classes
        TopNav topNav = new TopNav(driver);
        CheckoutPage checkout = new CheckoutPage(driver);
        CartPage cartPage = new CartPage(driver);
        OrderReceivedPage orderReceivedPage = new OrderReceivedPage(driver);
        MyAccountPage myAccountPage = new MyAccountPage(driver);

        


        [When(@"I successfully complete checkout")]
        public void WhenISuccessfullyCompleteCheckout()
        {
            ShopPage shopPage = new ShopPage(driver, topNav);

            shopPage.ViewCart();

            //Scrolling down to click the 'Proceed to checkout' button
            ScrollToElement(cartPage.SiteFooter);
            cartPage.ProceedToCheckout();

            List<string> billingDetails = new List<string>();
            billingDetails.Add(GetContextParameter("first_name"));
            billingDetails.Add(GetContextParameter("last_name"));
            billingDetails.Add(GetContextParameter("address_1"));
            billingDetails.Add(GetContextParameter("city"));
            billingDetails.Add(GetContextParameter("postcode"));
            billingDetails.Add(GetContextParameter("phone"));

            checkout.FillBillingDetails(billingDetails);
            checkout.SubmitOrder();
        }


        [Then(@"order number shown after checkout matches the one in Orders page")]
        public void ThenOrderNumberShownAfterCheckoutMatchesTheOneInOrdersPage()
        {
            //Capture the initial order number
            string orderNumberAtCheckout = "#" + orderReceivedPage.GetOrderNumberAtCheckout();
            //Take a screenshot of the initial order number displayed right after the checkout page
            TakeScreenshotOfElement("div[class='woocommerce-order']", "test2_initialorder");
            //Write the initial order number to console
            Console.WriteLine("Your order number is " + orderNumberAtCheckout);

            topNav.NavigateToMyAccount();
            //Navigate to 'Orders' tab on 'My account' page
            myAccountPage.ViewOrders();

            //Capture the order number present in order history
            string orderNumberFromHistory = myAccountPage.GetOrderNumberInHistory();
            //Take a screenshot of the order number present in the 'Orders' tab
            TakeScreenshotOfElement("tbody >tr", "test2_secondorder");
            //Write the order number from 'Orders' tab to console
            Console.WriteLine("Your order number from 'Orders' is " + orderNumberFromHistory);

            Assert.That(orderNumberAtCheckout, Is.EqualTo(orderNumberFromHistory), "Order number does not match");
        }
    }
}
