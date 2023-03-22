using TechTalk.SpecFlow;

namespace ecommerceFinalProject.Utils
{
    public class ContextDictionary
    {

        private readonly ScenarioContext _scenarioContext;
        
        public ContextDictionary(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        CartPage cartPage = new CartPage(TestBaseSpecflow.driver);
        TopNav topNav = new TopNav(TestBaseSpecflow.driver);
        MyAccountPage myAccountPage = new MyAccountPage(TestBaseSpecflow.driver);
        LoginPage loginPage = new LoginPage(TestBaseSpecflow.driver);
        OrderReceivedPage orderReceivedPage = new OrderReceivedPage(TestBaseSpecflow.driver);
        CheckoutPage checkoutPage = new CheckoutPage(TestBaseSpecflow.driver);
        ShopPage shopPage = new ShopPage(TestBaseSpecflow.driver);

        public void DefineContextDictionary()
        {
            _scenarioContext["cartPage"] = cartPage;
            _scenarioContext["topNav"] = topNav;
            _scenarioContext["myAccountPage"] = myAccountPage;
            _scenarioContext["loginPage"] = loginPage;
            _scenarioContext["orderReceivedPage"] = orderReceivedPage;
            _scenarioContext["checkoutPage"] = checkoutPage;
            _scenarioContext["shopPage"] = shopPage;
        }
    }
}
