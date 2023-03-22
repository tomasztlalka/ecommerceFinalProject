using static ecommerceFinalProject.Utils.TestBaseSpecflow;

namespace ecommerceFinalProject.StepDefinitions
{

    [Binding]
    public class CouponCodeStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        public CouponCodeStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"I apply the '([^']*)' coupon code during checkout")]
        public void WhenIApplyADiscountCodeDuringCheckout(string couponCode)
        {
            CartPage cartPage = (CartPage)_scenarioContext["cartPage"];
            ShopPage shopPage = (ShopPage)_scenarioContext["shopPage"];

            shopPage.ViewCart();                     
            cartPage.EnterCouponCode(couponCode);
        }


        [Then(@"the total amount is reduced by '([^']*)' percent")]
        public void ThenTheTotalAmountIsCorrectlyReduced(decimal discountPercentage)
        {
            CartPage cartPage = (CartPage)_scenarioContext["cartPage"];


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

            //Take a screenshot of the 'cart_totals' element and save it
            TakeScreenshotOfElement("div[class='cart_totals']", "test1_carttotals", true);

            //Assert that the two totals are the same
            Assert.That(actualTotal, Is.EqualTo(expectedTotal), "Actual total not equal to expected");
        }
    }
}
