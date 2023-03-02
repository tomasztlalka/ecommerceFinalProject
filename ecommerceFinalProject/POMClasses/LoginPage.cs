using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerceFinalProject.POMClasses
{
    internal class LoginPage
    {
        private IWebDriver _driver; //Field to hold a webdriver instance

        public LoginPage(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators
        public IWebElement usernameField => _driver.FindElement(By.Id("username"));
        public IWebElement passwordField => _driver.FindElement(By.Id("password"));
        public IWebElement submitButton => _driver.FindElement(By.Name("login"));

        //Service Methods
        public LoginPage SetUsername(string username)
        {
            usernameField.Clear();
            usernameField.SendKeys(username);
            return this;
        }

        public void SetPassword(string password)
        {
            passwordField.Clear();
            passwordField.SendKeys(password);
        }

        public void SubmitForm()
        {
            submitButton.Click();
        }

        //Helpers
        //public bool LoginExpectSuccess(string username, string password)
        //{
        //    SetUsername(username);
        //    SetPassword(password);
        //    SubmitForm();

        //    try
        //    {
        //        _driver.SwitchTo().Alert();
        //        Console.WriteLine("Alert present - not logged in");
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        //There is no alert - and that's OK!
        //    }
        //    return true;


        //}
    }
}
