
[TestFixture]

public class TestCase1 : TestBaseClass
{
    [Test]
    public void DiscountTest()
    {
        TopNav topNav = new TopNav(driver);

        //Defining an array of characters that need to be ignored when attempting to capture subtotal and total
        char[] charsToTrim = {'£'};

        //TODO: replace FindElement by POM method to make the line more readable
        double subTotal = double.Parse((driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span")).Text).Trim(charsToTrim), System.Globalization.CultureInfo.InvariantCulture);
        double fractionOfPriceAfterDiscount = 1.00 - double.Parse(TestContext.Parameters["discount_percentage"]);
        double shippingFee = double.Parse(TestContext.Parameters["shipping_fee"]);

        double expectedTotal = (subTotal * fractionOfPriceAfterDiscount) + shippingFee;
        Console.WriteLine("The expected total is: " + expectedTotal);

        //Wait for the coupon code text box to appear
        WaitForElement(By.Name("coupon_code"), 2, driver);
        
        //TODO: Implement this in a POM class
        //cart.EnterCouponCode().Apply();
        driver.FindElement(By.Name("coupon_code")).SendKeys(TestContext.Parameters["edgewords_coupon"]);
        driver.FindElement(By.Name("apply_coupon")).Click();

        //Wait for the coupon to get applied before proceeding further
        WaitForElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > th"), 2, driver);

        double actualTotal = double.Parse((driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span > bdi")).Text).Trim(charsToTrim), System.Globalization.CultureInfo.InvariantCulture);
        Console.WriteLine("The actual total is: " + actualTotal);

        Assert.That(expectedTotal == actualTotal, "Actual total different than expected total");


        //Attempt deleting the item from cart after each test is run, as items will remain in
        //cart even after logging out of the account
        try
        {
            topNav.Cart.Click();
            //Use Cart POM method
            driver.FindElement(By.CssSelector("#post-5 > div > div > form > table > tbody > tr.woocommerce-cart-form__cart-item.cart_item > td.product-remove > a")).Click();
        }
        catch
        {
            Console.WriteLine("No items in the cart to delete");
        }

        topNav.MyAccount.Click();
    }
}
