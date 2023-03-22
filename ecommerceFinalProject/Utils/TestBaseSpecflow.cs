using ecommerceFinalProject.POMClasses;

namespace ecommerceFinalProject.Utils
{

    [Binding]
    public class TestBaseSpecflow
    {
        public static IWebDriver driver;
        
        private readonly ScenarioContext _scenarioContext;
        //private readonly ShopPage _shopPage1;
        //private readonly CheckoutPage _checkoutPage;
        //private readonly CartPage _cartPage;
        //private readonly TopNav _topNav;
        //private readonly MyAccountPage _myAccountPage;
        //private readonly OrderReceivedPage _orderReceivedPage;

        public TestBaseSpecflow(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            //_shopPage1 = (ShopPage)_scenarioContext["shopPage"];
            //_checkoutPage = (CheckoutPage)_scenarioContext["checkoutPage"];
            //_cartPage = (CartPage)_scenarioContext["cartPage"];
            //_topNav = (TopNav)_scenarioContext["topNav"];
            //_myAccountPage = (MyAccountPage)_scenarioContext["myAccountPage"];
            //_orderReceivedPage = (OrderReceivedPage)_scenarioContext["orderReceivedPage"];
        }

        [BeforeScenario]
        public void SetUp()
        {
            //Instantiate driver based on string value in runsettings
            string driverBrowser = Environment.GetEnvironmentVariable("BROWSER").ToLower();
            switch (driverBrowser)
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "edge":
                    driver = new EdgeDriver();
                    break;
                case "headlesschrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--headless=new");
                    driver = new ChromeDriver(options);
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
            driver.Url = Environment.GetEnvironmentVariable("BASEURL");

            ContextDictionary cd = new ContextDictionary(_scenarioContext);
            cd.DefineContextDictionary();

            CartPage cartPage = (CartPage)_scenarioContext["cartPage"];

            //Dismissing the bottom blue bar - makes it easier to click and capture elements during tests
            cartPage.DismissNoticeBar();
        }

        [AfterScenario]
        public void TearDown()
        {
            CartPage cartPage = (CartPage)_scenarioContext["cartPage"];
            TopNav topNav = (TopNav)_scenarioContext["topNav"];
            MyAccountPage myAccountPage = (MyAccountPage)_scenarioContext["myAccountPage"];

            //Attempt to delete all items from the cart after every test is run
            cartPage.ClearCart();

            topNav.NavigateToMyAccount();

            //Logout done in TearDown as both test cases end by logging out
            myAccountPage.Logout();
            driver.Quit();
        }

        [Given(@"I am logged in as a user")]
        public void GivenIAmLoggedInAsAUser()
        {
            CartPage cartPage = (CartPage)_scenarioContext["cartPage"];
            TopNav topNav = (TopNav)_scenarioContext["topNav"];
            MyAccountPage myAccountPage = (MyAccountPage)_scenarioContext["myAccountPage"];
            LoginPage loginPage = (LoginPage)_scenarioContext["loginPage"];
          
            topNav.NavigateToMyAccount();
            loginPage.SetUsername(GetContextParameter("username"));
            loginPage.SetPassword(GetContextParameter("password"));
            loginPage.SubmitForm();

            //Assert that the login was successful
            Assert.That(myAccountPage.LogoutTab.Displayed, "Can't find the logout button - not logged in");

            //Attempt to delete all items from the cart before starting any tests
            cartPage.ClearCart();
        }

        [Given(@"I have added an '(.*)' to cart")]
        public void GivenIHaveAddedAnItemToCart(string item)
        {
            ShopPage shopPage = (ShopPage)_scenarioContext["shopPage"];
            //No need to check for null; AddItemToCart() already handles the null case
            shopPage.AddItemToCart(item);
        }

    }
}

