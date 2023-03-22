namespace ecommerceFinalProject.Utils
{

    [Binding]
    public class TestBaseSpecflow
    {
        public static IWebDriver driver;


        private readonly ScenarioContext _scenarioContext;
        public TestBaseSpecflow(ScenarioContext scenarioContext)
        {
            _scenarioContext= scenarioContext;
        }

        [BeforeScenario]
        public void SetUp()
        {
            //Instantiate driver based on string value in runsettings
            string driverTest = Environment.GetEnvironmentVariable("BROWSER").ToLower();
            switch (driverTest)
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

            CartPage cartPage = new CartPage(driver);
            TopNav topNav = new TopNav(driver);
            MyAccountPage myAccountPage = new MyAccountPage(driver);
            LoginPage loginPage = new LoginPage(driver);
            OrderReceivedPage orderReceivedPage = new OrderReceivedPage(driver);
            CheckoutPage checkoutPage = new CheckoutPage(driver);
            ShopPage shopPage = new ShopPage(driver);

            _scenarioContext["cartPage"] = cartPage;
            _scenarioContext["topNav"] = topNav;
            _scenarioContext["myAccountPage"] = myAccountPage;
            _scenarioContext["loginPage"] = loginPage;
            _scenarioContext["orderReceivedPage"] = orderReceivedPage;
            _scenarioContext["checkoutPage"] = checkoutPage;
            _scenarioContext["shopPage"] = shopPage;




            driver.Manage().Window.Maximize();
            driver.Url = Environment.GetEnvironmentVariable("BASEURL");
           
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



        

       
    }




}

