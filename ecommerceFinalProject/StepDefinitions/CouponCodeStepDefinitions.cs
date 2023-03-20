using OpenQA.Selenium;
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

        [When(@"I apply the '([^']*)' coupon code during checkout")]
        public void WhenIApplyADiscountCodeDuringCheckout(string couponCode)
        {
            ShopPage shopPage = new ShopPage(driver, topNav);

            //ViewCart proceeds to cartPage
            shopPage.ViewCart();                     
            cartPage.EnterCouponCode(TestContext.Parameters["coupon"]);
            
        }


        [Then(@"the total amount is reduced by '([^']*)' percent")]
        public void ThenTheTotalAmountIsCorrectlyReduced(decimal discountPercentage)
        {
            //Thread.Sleep(3000);
            //Scrolling down to 'prepare' for a screenshot
            ScrollToBottom();
            //ScrollToElement(cartPage.SiteFooter);
            //Thread.Sleep(1000);

            decimal subTotal = decimal.Parse((cartPage.SubTotal.Text).Trim(charsToTrim));
            //TODO: Write a method GetParameter(string parameter) that does null checking and parses the string into TC.P
            //TODO: MOVE THIS INTO A SEPARATE METHOD IN CARTPAGE POM - CalculateExpectedTotal()
            //Capture subtotal value from page and get values of parameters from the runsettings file

            decimal discountAsFloat = (decimal.Parse(CheckNull(TestContext.Parameters["discount_percentage"]))) / 100;
            decimal shippingFee = decimal.Parse(CheckNull(TestContext.Parameters["shipping_fee"]));
            //Work out the expected total
            expectedTotal = (subTotal * (1 - discountAsFloat)) + shippingFee;


            //Defining decimal point precision to format output in the console
            setPrecision.NumberDecimalDigits = 2;
            //Write the total to console for debugging purposes
            Console.WriteLine("The expected total is: " + expectedTotal.ToString("N", setPrecision));





            //Take a screenshot of the 'cart_totals' element and save it
            TakeScreenshotOfElement("div[class='cart_totals']", "test1_carttotals");

            //Capture the actual total from page
            actualTotal = decimal.Parse((cartPage.CartTotal.Text).Trim(charsToTrim));
            //Write the total to console for debugging purposes
            Console.WriteLine("The actual total is: " + actualTotal.ToString("N", setPrecision));
            //Assert that the two totals are the same
            Assert.That(expectedTotal, Is.EqualTo(actualTotal), "Actual total different than expected");

            
        }


        
    }
}
