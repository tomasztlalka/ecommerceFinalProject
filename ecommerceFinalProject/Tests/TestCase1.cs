
[TestFixture]
public class TestCase1 : TestBaseClass
{
    [Test]
    public void DiscountTest()
    {
        TopNav topNav = new TopNav(driver);
        CartPage cartPage = new CartPage(driver);
        ShopPage shopPage = new ShopPage(driver, topNav);

        //Defining decimal point precision
        NumberFormatInfo setPrecision = new NumberFormatInfo();
        setPrecision.NumberDecimalDigits = 2;
        
        //Defining an array of characters that need to be ignored when attempting to capture subtotal and total
        char[] charsToTrim = {'£'};

        shopPage.AddItemToCart();
        shopPage.ViewCart();

        //Capture subtotal value from page and get values of parameters from the runsettings file
        decimal subTotal = decimal.Parse((cartPage.SubTotal.Text).Trim(charsToTrim));
        decimal fractionOfPriceAfterDiscount = 1 - decimal.Parse(TestContext.Parameters["discount_percentage"]);
        decimal shippingFee = decimal.Parse(TestContext.Parameters["shipping_fee"]);

        //Work out the expected total
        decimal expectedTotal = (subTotal * fractionOfPriceAfterDiscount) + shippingFee;
        //Write to console for debugging purposes
        Console.WriteLine("The expected total is: " + expectedTotal.ToString("N", setPrecision));

        cartPage.EnterCouponCode();        
        
        //Capture the actual total from page
        decimal actualTotal = decimal.Parse((cartPage.CartTotal.Text).Trim(charsToTrim));
        //Write to console for debugging purposes
        Console.WriteLine("The actual total is: " + actualTotal.ToString("N", setPrecision));
        //Assert that the two totals are the same
        Assert.That(expectedTotal == actualTotal, "Actual total different than expected total");
    }
}
