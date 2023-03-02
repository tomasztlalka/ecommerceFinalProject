using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Checkout
{
    private IWebDriver _driver; //Field to hold a webdriver instance

    public Checkout(IWebDriver driver) //Get the webdriver instance from the calling test
    {
        this._driver = driver;
    }

    //driver.FindElement(By.Id("billing_first_name")).Clear();
    //driver.FindElement(By.Id("billing_first_name")).SendKeys(TestContext.Parameters["first_name"]);

    //driver.FindElement(By.Id("billing_last_name")).Clear();
    //driver.FindElement(By.Id("billing_last_name")).SendKeys(TestContext.Parameters["last_name"]);

    //driver.FindElement(By.Id("billing_address_1")).Clear();
    //driver.FindElement(By.Id("billing_address_1")).SendKeys(TestContext.Parameters["address_1"]);

    //driver.FindElement(By.Id("billing_city")).Clear();
    //driver.FindElement(By.Id("billing_city")).SendKeys(TestContext.Parameters["city"]);

    //driver.FindElement(By.Id("billing_postcode")).Clear();
    //driver.FindElement(By.Id("billing_postcode")).SendKeys(TestContext.Parameters["postcode"]);

    //driver.FindElement(By.Id("billing_phone")).Clear();
    //driver.FindElement(By.Id("billing_phone")).SendKeys(TestContext.Parameters["phone"]);

    //Locators
    public IWebElement firstName => _driver.FindElement(By.Id("billing_first_name"));
    public IWebElement lastName => _driver.FindElement(By.Id("billing_last_name"));
    public IWebElement billingAddress => _driver.FindElement(By.Id("billing_address_1"));
    public IWebElement city => _driver.FindElement(By.Id("billing_city"));
    public IWebElement postcode => _driver.FindElement(By.Id("billing_postcode"));
    public IWebElement phone => _driver.FindElement(By.Id("billing_first_name"));
    public IWebElement placeOrderButton => _driver.FindElement(By.CssSelector("#place_order"));

    
    

    //Service Methods
    public Checkout FillBillingDetails()
    {
        firstName.Clear();
        lastName.Clear();
        billingAddress.Clear();
        city.Clear();
        postcode.Clear();
        phone.Clear();

        firstName.SendKeys(TestContext.Parameters["first_name"]);
        lastName.SendKeys(TestContext.Parameters["last_name"]);
        billingAddress.SendKeys(TestContext.Parameters["address_1"]);
        city.SendKeys(TestContext.Parameters["city"]);
        postcode.SendKeys(TestContext.Parameters["postcode"]);
        phone.SendKeys(TestContext.Parameters["phone"]);
        Thread.Sleep(500);

        return this;
    }

        

    public void SubmitOrder()
    {
        placeOrderButton.Click();
    }

        
}

