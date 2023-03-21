using OpenQA.Selenium;
using static ecommerceFinalProject.Utils.TestBaseSpecflow;

namespace ecommerceFinalProject.StepDefinitions
{

    [Binding]
    public class CouponCodeStepDefinitions
    {
        TopNav topNav = new TopNav(driver);
        CartPage cartPage = new CartPage(driver);

        //Defining an array of characters that need to be ignored when attempting to capture subtotal and total
        
        [When(@"I apply the '([^']*)' coupon code during checkout")]
        public void WhenIApplyADiscountCodeDuringCheckout(string couponCode)
        {
            ShopPage shopPage = new ShopPage(driver, topNav);
            shopPage.ViewCart();                     
            cartPage.EnterCouponCode(couponCode);
        }


        [Then(@"the total amount is reduced by '([^']*)' percent")]
        public void ThenTheTotalAmountIsCorrectlyReduced(decimal discountPercentage)
        {
            //Work out the expected total
            decimal expectedTotal = cartPage.CalculateExpectedTotal(discountPercentage);
            //Write the total to console for debugging purposes
            Console.Write("Expected total is: ");
            //Specifying string format
            Console.WriteLine("{0:00.00}", expectedTotal);

            //Capture the actual total from page
            decimal actualTotal = cartPage.GetActualTotal();
            //Write the total to console for debugging purposes
            Console.WriteLine("The actual total is: " + actualTotal);

            //Need to wait for element to be in sight
            Thread.Sleep(1000);
            //Take a screenshot of the 'cart_totals' element and save it
            TakeScreenshotOfElement("div[class='cart_totals']", "test1_carttotals");

            //Assert that the two totals are the same
            Assert.That(actualTotal, Is.EqualTo(expectedTotal), "Actual total not equal to expected");
        }
    }
}
