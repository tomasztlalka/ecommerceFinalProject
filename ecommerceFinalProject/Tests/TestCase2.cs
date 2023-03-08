/*
 *Next steps:
 *Write to results
 */

[TestFixture]
public class TestCase2 : TestBaseClass
{
    [Test]
    public void OrderNumberTest()
    {
        //TODO: See if this can be done differently (not instantiating all the classes in every test?)
        TopNav topNav = new TopNav(driver);
        ShopPage shopPage = new ShopPage(driver, topNav);
        CheckoutPage checkout = new CheckoutPage(driver);
        CartPage cartPage = new CartPage(driver);
        OrderReceivedPage orderReceivedPage = new OrderReceivedPage(driver);
        MyAccountPage myAccountPage = new MyAccountPage(driver);

        shopPage.AddItemToCart();
        shopPage.ViewCart();

        //Scrolling down to click the 'Proceed to checkout' button
        cartPage.ScrollToElement(cartPage.SiteFooter);
        cartPage.ProceedToCheckoutButton.Click();

        checkout.FillBillingDetails();
        checkout.SubmitOrder();
           
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

