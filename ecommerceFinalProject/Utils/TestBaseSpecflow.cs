using ecommerceFinalProject.POMClasses;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;


namespace ecommerceFinalProject.Utils
{

    [Binding]
    public class TestBaseSpecflow
    {
        public static IWebDriver driver;

        
        [BeforeScenario]
        public void SetUp()
        {
            //Instantiate driver based on string value in runsettings
            string driverTest = CheckNull(TestContext.Parameters["browser"]).ToLower();
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
                default:
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
            driver.Url = TestContext.Parameters["baseURL"];
            CartPage cartPage = new CartPage(driver);

            //Dismissing the bottom blue bar - makes it easier to click and capture elements during tests
            cartPage.DismissNoticeBar();
        }


        [AfterScenario]
        public void TearDown()
        {
            TopNav topNav = new TopNav(driver);
            MyAccountPage myAccountPage = new MyAccountPage(driver);
            CartPage cartPage = new CartPage(driver);

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
            TopNav topNav = new TopNav(driver);
            LoginPage login = new LoginPage(driver);
            MyAccountPage myAccountPage = new MyAccountPage(driver);
            CartPage cartPage = new CartPage(driver);

            topNav.NavigateToMyAccount();

            login.SetUsername(TestContext.Parameters["username"]);
            login.SetPassword(TestContext.Parameters["password"]);
            login.SubmitForm();

            //Assert that the login was successful
            Assert.That(myAccountPage.LogoutTab.Displayed, "Can't find the logout button - not logged in");

            //Attempt to delete all items from the cart before starting any tests
            cartPage.ClearCart();
        }




    }




}

