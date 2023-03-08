

internal class LoginPage
{
    private IWebDriver _driver; //Field to hold a webdriver instance

    public LoginPage(IWebDriver driver) //Get the webdriver instance from the calling test
    {
        this._driver = driver;
    }

    //Locators
    public IWebElement UsernameField => _driver.FindElement(By.Id("username"));
    public IWebElement PasswordField => _driver.FindElement(By.Id("password"));
    public IWebElement SubmitButton => _driver.FindElement(By.Name("login"));

    //Service Methods
    public LoginPage SetUsername(string username)
    {
        UsernameField.Clear();
        UsernameField.SendKeys(username);
        return this;
    }

    public void SetPassword(string password)
    {
        PasswordField.Clear();
        PasswordField.SendKeys(password);
    }

    public void SubmitForm()
    {
        SubmitButton.Click();
    }

}

