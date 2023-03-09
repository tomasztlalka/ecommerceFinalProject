using System;
using TechTalk.SpecFlow;
using static ecommerceFinalProject.Utils.TestBaseSpecflow;
using ecommerceFinalProject.POMClasses;

namespace ecommerceFinalProject.StepDefinitions
{
    [Binding]
    public class Feature2StepDefinitions
    {
        TopNav topNav = new TopNav(driver);
        CheckoutPage checkout = new CheckoutPage(driver);
        CartPage cartPage = new CartPage(driver);
        OrderReceivedPage orderReceivedPage = new OrderReceivedPage(driver);
        MyAccountPage myAccountPage = new MyAccountPage(driver);


        [When(@"I successfully complete checkout")]
        public void WhenISuccessfullyCompleteCheckout()
        {
            //TODO: See if this can be done differently (not instantiating all the classes in every test?)
            ShopPage shopPage = new ShopPage(driver, topNav);

            shopPage.AddItemToCart();
            shopPage.ViewCart();

            //Scrolling down to click the 'Proceed to checkout' button
            cartPage.ScrollToElement(cartPage.SiteFooter);
            cartPage.ProceedToCheckoutButton.Click();

            checkout.FillBillingDetails();
            checkout.SubmitOrder();
        }

        [Then(@"order number shown after checkout matches the one in '([^']*)' page")]
        public void ThenOrderNumberShownAfterCheckoutMatchesTheOneInPage(string orders)
        {
            //Capture the initial order number
            string orderNumber1 = "#" + orderReceivedPage.DisplayedOrderNumber.Text;
            //Write the initial order number to console for debugging purposes
            Console.WriteLine("Your order number is " + orderNumber1);

            topNav.MyAccount.Click();

            //Navigate to 'Orders' tab on 'My account' page
            myAccountPage.OrdersTab.Click();
            string orderNumber2 = myAccountPage.OrderNumber.Text;

            Console.WriteLine("Your order number from 'Orders' is " + orderNumber2);
            Assert.That(orderNumber1 == orderNumber2, "Order number does not match");
        }
    }
}
