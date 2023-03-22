using Gherkin.Ast;
using static ecommerceFinalProject.Utils.TestBaseSpecflow;

namespace ecommerceFinalProject.StepDefinitions
{
    [Binding]
    public class OrderNumbersStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        public OrderNumbersStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }

        [Given(@"I have added an '(.*)' to cart")]
        public void GivenIHaveAddedAnItemToCart(string item)
        {
            ShopPage shopPage = (ShopPage)_scenarioContext["shopPage"];
            //No need to check for null; AddItemToCart() already handles the null case
            shopPage.AddItemToCart(item);
        }




        [When(@"I successfully complete checkout using these details")]
        public void WhenISuccessfullyCompleteCheckoutUsingTheseDetails(Table table)
        {
            ShopPage shopPage = (ShopPage)_scenarioContext["shopPage"];
            CheckoutPage checkoutPage = (CheckoutPage)_scenarioContext["checkoutPage"];
            CartPage cartPage = (CartPage)_scenarioContext["cartPage"];

            shopPage.ViewCart();
            cartPage.ProceedToCheckout();

            List<string> billingDetails = new List<string>();
            foreach (TechTalk.SpecFlow.TableRow row in table.Rows)
            {
                billingDetails.Add(row["first_name"]);
                billingDetails.Add(row["last_name"]);
                billingDetails.Add(row["address_line1"]);
                billingDetails.Add(row["city"]);
                billingDetails.Add(row["postcode"]);
                billingDetails.Add(row["phone"]);
            }
            checkoutPage.FillBillingDetails(billingDetails);
            checkoutPage.SubmitOrder();
        }




        [Then(@"order number shown after checkout matches the one in Orders page")]
        public void ThenOrderNumberShownAfterCheckoutMatchesTheOneInOrdersPage()
        {
            TopNav topNav = (TopNav)_scenarioContext["topNav"];
            MyAccountPage myAccountPage = (MyAccountPage)_scenarioContext["myAccountPage"];
            OrderReceivedPage orderReceivedPage = (OrderReceivedPage)_scenarioContext["orderReceivedPage"];


            //Capture the initial order number
            string orderNumberAtCheckout = orderReceivedPage.GetOrderNumberAtCheckout();
            //Take a screenshot of the initial order number displayed right after the checkout page
            TakeScreenshotOfElement("div[class='woocommerce-order']", "test2_initialorder");
            //Write the initial order number to console
            Console.WriteLine("Order number at checkout is " + orderNumberAtCheckout);

            topNav.NavigateToMyAccount();
            //Navigate to 'Orders' tab on 'My account' page
            myAccountPage.ViewOrders();

            //Capture the order number present in order history
            string orderNumberFromHistory = myAccountPage.GetOrderNumberInHistory();
            //Take a screenshot of the order number present in the 'Orders' tab
            TakeScreenshotOfElement("tbody >tr", "test2_secondorder");
            //Write the order number from 'Orders' tab to console
            Console.WriteLine("Order number from 'Orders' is " + orderNumberFromHistory);

            Assert.That(orderNumberAtCheckout, Is.EqualTo(orderNumberFromHistory), "Order number does not match");
        }
    }
}
