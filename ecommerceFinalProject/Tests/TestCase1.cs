
[TestFixture]

public class TestCase1 : TestBaseClass
{
    [Test]
    public void DiscountTest()
    {
        TopNav topNav = new TopNav(driver);
        CartPage cartPage = new CartPage(driver);

        //Defining an array of characters that need to be ignored when attempting to capture subtotal and total
        char[] charsToTrim = {'£'};

        double subTotal = double.Parse((cartPage.subTotal.Text).Trim(charsToTrim));
        double fractionOfPriceAfterDiscount = 1.00 - double.Parse(TestContext.Parameters["discount_percentage"]);
        double shippingFee = double.Parse(TestContext.Parameters["shipping_fee"]);

        double expectedTotal = (subTotal * fractionOfPriceAfterDiscount) + shippingFee;
        Console.WriteLine("The expected total is: " + expectedTotal);

        //Wait for the coupon code text box to appear
        WaitForElement(By.Name("coupon_code"), 2, driver);
        cartPage.EnterCouponCode();
        
        
        //Wait for the coupon to get applied before proceeding further
        WaitForElement(By.CssSelector(cartPage.appliedCouponFieldPath), 2, driver);

        double actualTotal = double.Parse((cartPage.cartTotal.Text).Trim(charsToTrim));
        Console.WriteLine("The actual total is: " + actualTotal);

        Assert.That(expectedTotal == actualTotal, "Actual total different than expected total");

        //Attempt deleting the item from cart after each test is run, as items will remain in
        //cart even after logging out of the account
        try
        {
            topNav.Cart.Click();
            cartPage.DeleteItem();
        }
        catch
        {
            Console.WriteLine("No items in the cart to delete");
        }

        topNav.MyAccount.Click();
    }
}
