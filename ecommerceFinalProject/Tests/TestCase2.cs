/*
 *Next steps:
 *Write to results
 */


using OpenQA.Selenium.Interactions;

[TestFixture]
public class TestCase2 : TestBaseClass
{
    [Test]
    public void OrderNumberTest()
    {
        //Scrolling down to be able to click 'Proceed to checkout' button
        var element = driver.FindElement(By.CssSelector("#colophon > div > div.site-info"));
        Actions actions = new Actions(driver);
        actions.MoveToElement(element).Perform();
        
        driver.FindElement(By.LinkText("Proceed to checkout")).Click();

        Checkout checkout = new Checkout(driver);
        checkout.FillBillingDetails().SubmitOrder();
                
        //Wait for order number to appear on page
        WaitForElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"), 3, driver);

        string orderNumber1 = "#" + driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong")).Text;
        Console.WriteLine("Your order number is " + orderNumber1);

        //TODO: See if this can be done differently (not instantiating a topNav in every class?)
        TopNav topNav = new TopNav(driver);
        topNav.MyAccount.Click();

        //Navigate to 'Orders' tab on 'My account' page
        driver.FindElement(By.CssSelector("#post-7 > div > div > nav > ul > li.woocommerce-MyAccount-navigation-link.woocommerce-MyAccount-navigation-link--orders > a")).Click();
        string orderNumber2 = driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a")).Text;
        Console.WriteLine("Your order number from 'Orders' is " + orderNumber2);
        Assert.That(orderNumber1 == orderNumber2, "Order number does not match");
    }
}

