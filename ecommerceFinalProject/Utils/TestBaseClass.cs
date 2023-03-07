/*
*Next steps:
* TODO: figure out Locator strategy throughout the webpages that is more readable
*/

using ecommerceFinalProject.POMClasses;

public class TestBaseClass
{
    public IWebDriver driver;
    
    [SetUp]
    public void Setup()
    {
        //TODO: Figure out how to escape the slash in runsettings and move
        string exampleItemPath = "//*[@id=\"main\"]/ul/li[3]/a[2]";

        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        LoginPage login = new LoginPage(driver);
        TopNav topNav = new TopNav(driver);
        
        driver.Url = TestContext.Parameters["baseURL"];
        driver.FindElement(By.Id("menu-item-46")).Click();

        //Using POM methods to login
        login.SetUsername(TestContext.Parameters["username"]);
        login.SetPassword(TestContext.Parameters["password"]);
        login.SubmitForm();

        //Assert that the login was successful
        //TODO: Move to POM
        Assert.That(driver.FindElement(By.LinkText("Logout")).Displayed, "Can't find the logout button - not logged in");
        
        RemoveItemFromCart();        
        topNav.Shop.Click();

        //Add an item of clothing to the cart
        //TODO: Move to POM
        driver.FindElement(By.XPath(exampleItemPath)).Click();

        //Wait for the cart to get updated with the newly added item
        WaitForElement(By.LinkText("View cart"), 2, driver);
        topNav.Cart.Click();
    } 

    [TearDown] 
    public void TearDown() 
    {
        TopNav topNav = new TopNav(driver);
        RemoveItemFromCart();
        topNav.MyAccount.Click();

        //Logout done in TearDown as both test cases end by logging out
        driver.FindElement(By.LinkText("Logout")).Click();
        driver.Quit();
    }

    //Attempt deleting the item from cart after each test is run, as items will remain in
    //cart even after logging out of the account
    public void RemoveItemFromCart()
    {
        TopNav topNav = new TopNav(driver);
        CartPage cartPage = new CartPage(driver);
        try
        {
            topNav.Cart.Click();
            cartPage.DeleteItem();
        }
        catch
        {
            Console.WriteLine("No items in the cart to delete");
        }
    }
}

