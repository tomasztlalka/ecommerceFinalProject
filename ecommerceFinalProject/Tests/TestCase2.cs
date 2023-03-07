/*
 *Next steps:
 *Write to results
 */


using OpenQA.Selenium.DevTools.V108.CacheStorage;
using OpenQA.Selenium.Interactions;

[TestFixture]
public class TestCase2 : TestBaseClass
{
    [Test]
    public void OrderNumberTest()
    {
        //TODO: See if this can be done differently (not instantiating a topNav in every class?)
        TopNav topNav = new TopNav(driver);
        ShopPage shopPage = new ShopPage(driver, topNav);
        CheckoutPage checkout = new CheckoutPage(driver);
        CartPage cartPage = new CartPage(driver);
        OrderReceivedPage orderReceivedPage = new OrderReceivedPage(driver);
        MyAccountPage myAccountPage = new MyAccountPage(driver);

        //TODO: Use TestContext.Parameters["item_Path"]
        shopPage.AddItemToCart("//*[@id=\"main\"]/ul/li[3]/a[2]");
        shopPage.ViewCart();

        //Scrolling down to click the 'Proceed to checkout' button
        cartPage.ScrollToElement(cartPage.siteFooter);
        cartPage.proceedToCheckoutButton.Click();

        checkout.FillBillingDetails();
        checkout.SubmitOrder();
                
        //Wait for order number to appear on page
        WaitForElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"), 3, driver);

        //Capture the initial order number
        string orderNumber1 = "#" + orderReceivedPage.displayedOrderNumber.Text;
        //Write the initial order number to console for debugging purposes
        Console.WriteLine("Your order number is " + orderNumber1);

        topNav.MyAccount.Click();

        //Navigate to 'Orders' tab on 'My account' page
        myAccountPage.ordersButton.Click();
        string orderNumber2 = myAccountPage.orderNumber.Text;
        
        Console.WriteLine("Your order number from 'Orders' is " + orderNumber2);
        Assert.That(orderNumber1 == orderNumber2, "Order number does not match");
    }
}

