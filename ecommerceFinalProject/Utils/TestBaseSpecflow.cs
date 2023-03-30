using ecommerceFinalProject.POMClasses;
using TechTalk.SpecFlow.Infrastructure;

namespace ecommerceFinalProject.Utils
{

    [Binding]
    public class TestBaseSpecflow
    {
        private IWebDriver driver;
        
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _outputHelper;
        
        public TestBaseSpecflow(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
        {
            _scenarioContext = scenarioContext;
            _outputHelper = outputHelper;

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

            //Add the driver and the output helper to scenarioContext dictionary
            _scenarioContext["driver"] = driver;
            _scenarioContext["output"] = _outputHelper;
            

            driver.Manage().Window.Maximize();
            driver.Url = Environment.GetEnvironmentVariable("BASEURL");

            CartPage cartPage = new CartPage(driver);
            //Dismissing the bottom blue bar - makes it easier to click and capture elements during tests
            cartPage.DismissNoticeBar();
        }

        [AfterScenario]
        public void TearDown()
        {
            CartPage cartPage = new CartPage(driver);
            //Attempt to delete all items from the cart after every test is run
            cartPage.ClearCart();

            TopNav topNav = new TopNav(driver);
            topNav.NavigateToMyAccount();

            MyAccountPage myAccountPage = new MyAccountPage(driver);
            //Logout done in TearDown as both test cases end by logging out
            myAccountPage.Logout();
            driver.Quit();
        }

        [Given(@"I am logged in as a user")]
        public void GivenIAmLoggedInAsAUser()
        {
            TopNav topNav = new TopNav(driver);
            topNav.NavigateToMyAccount();

            LoginPage loginPage = new LoginPage(driver);
            loginPage.SetUsername(GetContextParameter("username"));
            loginPage.SetPassword(GetContextParameter("password"));
            loginPage.SubmitForm();

            MyAccountPage myAccountPage = new MyAccountPage(driver);
            //Assert that the login was successful
            Assert.That(myAccountPage.LogoutTab.Displayed, "Can't find the logout button - not logged in");

            CartPage cartPage = new CartPage(driver);
            //Attempt to delete all items from the cart before starting any tests
            cartPage.ClearCart();
        }

        [Given(@"I have added an '(.*)' to cart")]
        public void GivenIHaveAddedAnItemToCart(string item)
        {
            ShopPage shopPage = new ShopPage(driver, _outputHelper);
            //No need to check for null; AddItemToCart() already handles the null case
            shopPage.AddItemToCart(item);
        }

    }
}

