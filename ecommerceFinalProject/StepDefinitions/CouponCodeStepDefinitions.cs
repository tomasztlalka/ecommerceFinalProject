using static ecommerceFinalProject.Utils.TestBaseSpecflow;

namespace ecommerceFinalProject.StepDefinitions
{

    [Binding]
    public class CouponCodeStepDefinitions
    {
        NumberFormatInfo setPrecision = new NumberFormatInfo();
        TopNav topNav = new TopNav(driver);
        CartPage cartPage = new CartPage(driver);

        //Defining an array of characters that need to be ignored when attempting to capture subtotal and total
        char[] charsToTrim = { '£' };

        decimal expectedTotal;
        decimal actualTotal;



        [When(@"I apply a discount code during checkout")]
        public void WhenIApplyADiscountCodeDuringCheckout()
        {
            ShopPage shopPage = new ShopPage(driver, topNav);

            shopPage.AddItemToCart();

            //ViewCart proceeds to cartPage
            shopPage.ViewCart();

            //Capture subtotal value from page and get values of parameters from the runsettings file
            decimal subTotal = decimal.Parse((cartPage.SubTotal.Text).Trim(charsToTrim));
            decimal fractionOfPriceAfterDiscount = 1 - decimal.Parse(TestContext.Parameters["discount_percentage"]);
            decimal shippingFee = decimal.Parse(TestContext.Parameters["shipping_fee"]);

            //Work out the expected total
            expectedTotal = (subTotal * fractionOfPriceAfterDiscount) + shippingFee;

            //Defining decimal point precision to format output in the console
            setPrecision.NumberDecimalDigits = 2;
            //Write the total to console for debugging purposes
            Console.WriteLine("The expected total is: " + expectedTotal.ToString("N", setPrecision));
                     
            cartPage.EnterCouponCode();
            
        }


        [Then(@"the total amount is correctly reduced")]
        public void ThenTheTotalAmountIsCorrectlyReduced()
        {
            //Scrolling down to 'prepare' for a screenshot
            ScrollToElement(driver, cartPage.SiteFooter);
            Thread.Sleep(1000);
            //Take a screenshot of the 'cart_totals' element and save it
            TakeScreenshotOfElement(driver, By.CssSelector("div[class='cart_totals']"), "test1_carttotals.png");

            //Capture the actual total from page
            actualTotal = decimal.Parse((cartPage.CartTotal.Text).Trim(charsToTrim));
            //Write the total to console for debugging purposes
            Console.WriteLine("The actual total is: " + actualTotal.ToString("N", setPrecision));
            //Assert that the two totals are the same
            Assert.That(expectedTotal == actualTotal, "Actual total different than expected total");

            
        }


        
    }
}
