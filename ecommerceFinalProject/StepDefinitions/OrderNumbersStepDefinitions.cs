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

            shopPage.AddItemToCart();
            shopPage.ViewCart();

            //Scrolling down to click the 'Proceed to checkout' button
            ScrollToElement(driver, cartPage.SiteFooter);
            cartPage.ProceedToCheckoutButton.Click();

            checkout.FillBillingDetails();
            checkout.SubmitOrder();
        }

        [Then(@"order number shown after checkout matches the one in Orders page")]
        public void ThenOrderNumberShownAfterCheckoutMatchesTheOneInOrdersPage()
        {
            //Capture the initial order number
            string orderNumber1 = "#" + orderReceivedPage.DisplayedOrderNumber.Text;

            //Take a screenshot of the initial order number displayed right after the checkout page
            TakeScreenshotOfElement(driver, By.CssSelector("div[class='woocommerce-order']"), "test2_initialorder.png");

            //Write the initial order number to console for debugging purposes
            Console.WriteLine("Your order number is " + orderNumber1);

            topNav.MyAccount.Click();

            //Navigate to 'Orders' tab on 'My account' page
            myAccountPage.OrdersTab.Click();
            string orderNumber2 = myAccountPage.OrderNumber.Text;

            //Take a screenshot of the order number present in the 'Orders' tab
            TakeScreenshotOfElement(driver, By.CssSelector("tbody >tr"), "test2_secondorder.png");

            //Write the order number from 'Orders' tab to console for debugging purposes
            Console.WriteLine("Your order number from 'Orders' is " + orderNumber2);
            Assert.That(orderNumber1 == orderNumber2, "Order number does not match");
        }
    }
}
