/*
*Next steps:
* TODO: figure out Locator strategy throughout the webpages that is more readable
*/

public class TestBaseClass
{
    public IWebDriver driver;
    
    [SetUp]
    public void Setup()
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
        Assert.That(myAccountPage.logoutTab.Displayed, "Can't find the logout button - not logged in");
        
        //Attempt deleting item from cart before starting any tests
        RemoveItemFromCart();   
    } 

    [TearDown] 
    public void TearDown() 
    {
        TopNav topNav = new TopNav(driver);
        MyAccountPage myAccountPage = new MyAccountPage(driver);


        //Attempt deleting the item from cart after each test is run, as items will remain in
        //cart even after logging out of the account
        RemoveItemFromCart();

        topNav.MyAccount.Click();

        //Logout done in TearDown as both test cases end by logging out
        myAccountPage.logoutTab.Click();
        driver.Quit();
    }

    //Method for navigating to the Cart and removing any item
    public void RemoveItemFromCart()
    {
        TopNav topNav = new TopNav(driver);
        CartPage cartPage = new CartPage(driver);


        topNav.Cart.Click();

        //TODO: This needs fixing, then rename method ^
        //while (cartPage.couponField.Displayed)
        //{
            try
            {
                cartPage.DeleteItem();

                WaitForElement((By)cartPage.deleteButton, 3, driver);
                

            }
            catch
            {
                Console.WriteLine("No items in the cart to delete");
            }
            
        //}
       
    }
}

