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
            //Thread.Sleep(3000);
            //Scrolling down to 'prepare' for a screenshot
            ScrollToElement(cartPage.SiteFooter);
            Thread.Sleep(1000);


            //Work out the expected total
            decimal expectedTotal = cartPage.CalculateExpectedTotal(discountPercentage);
            //Write the total to console for debugging purposes
            Console.WriteLine("Expected total is : " + expectedTotal);

            //Take a screenshot of the 'cart_totals' element and save it
            TakeScreenshotOfElement("div[class='cart_totals']", "test1_carttotals");

            //Capture the actual total from page
            decimal actualTotal = cartPage.GetActualTotal();
            //Write the total to console for debugging purposes
            Console.WriteLine("The actual total is: " + actualTotal);

            //Assert that the two totals are the same
            Assert.That(actualTotal, Is.EqualTo(expectedTotal), "Actual total different than expected");

            
        }


        
    }
}
