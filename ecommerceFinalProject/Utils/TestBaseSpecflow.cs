using System.Configuration;
using TechTalk.SpecFlow;

namespace ecommerceFinalProject.Utils
{

    [Binding]
    public class TestBaseSpecflow
    {
        public static IWebDriver driver;

        
        [BeforeScenario]
            

        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = TestContext.Parameters["baseURL"];

            TopNav topNav = new TopNav(driver);
            LoginPage login = new LoginPage(driver);
            MyAccountPage myAccountPage = new MyAccountPage(driver);

            topNav.MyAccount.Click();

            login.SetUsername(TestContext.Parameters["username"]);
            login.SetPassword(TestContext.Parameters["password"]);
            login.SubmitForm();

            //Assert that the login was successful
            Assert.That(myAccountPage.LogoutTab.Displayed, "Can't find the logout button - not logged in");

            //Attempt to delete all items from the cart before starting any tests
            ClearCart();
        }


        [AfterScenario]
        public void TearDown()
        {
            TopNav topNav = new TopNav(driver);
            MyAccountPage myAccountPage = new MyAccountPage(driver);


            //Attempt to delete all items from the cart after every test is run
            ClearCart();

            topNav.MyAccount.Click();

            //Logout done in TearDown as both test cases end by logging out
            myAccountPage.LogoutTab.Click();
            driver.Quit();
        }

        //Method for navigating to the Cart and removing any item
        public void ClearCart()
        {
            TopNav topNav = new TopNav(driver);
            CartPage cartPage = new CartPage(driver);


            topNav.Cart.Click();

            try
            {
                //while (cartPage.CartEmptyMessage.Displayed)
                while (true) 
                {
                    cartPage.DeleteItem();
                    //WaitForElement(By.CssSelector(cartPage.DeleteButtonPath), 3, driver);
                    Thread.Sleep(1000);
                }
            }
            catch 
            {
                //Do nothing - cart is cleared
            }


        }

    }
}

