using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ecommerceFinalProject.POMClasses
{
    internal class CartPage
    {

        private IWebDriver _driver; //Field to hold a webdriver instance


        public CartPage(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;

        }

        //Locators

        public IWebElement CouponField => _driver.FindElement(By.Name("coupon_code"));

        public IWebElement ApplyButton => _driver.FindElement(By.Name("apply_coupon"));

        public IWebElement ProceedToCheckoutButton => _driver.FindElement(By.LinkText("Proceed to checkout"));

        public IWebElement SiteFooter => _driver.FindElement(By.CssSelector("div[class='site-info']"));

        public IWebElement DeleteButton => _driver.FindElement(By.CssSelector("a[class='remove']"));

        public IWebElement SubTotal => _driver.FindElement(By.CssSelector("td[data-title='Subtotal']"));

        public IWebElement CartTotal => _driver.FindElement(By.CssSelector("td[data-title='Total']"));

        public IWebElement CartEmptyMessage => _driver.FindElement(By.CssSelector("div[class='woocommerce-notices-wrapper']"));

        public IWebElement ReturnToShopButton => _driver.FindElement(By.CssSelector("a[class='button wc-backward']"));

        public string DeleteButtonPath => "a[class='remove']";  //remove
                                                                //Service Methods

        public void EnterCouponCode()
        {
            //Wait for the coupon code text box to appear
            WaitForElement(By.Name("coupon_code"), 2, _driver);
            CouponField.SendKeys(TestContext.Parameters["edgewords_coupon"]);
            ApplyButton.Click();

            //Wait for the coupon to get applied before proceeding further
            WaitForElement(By.CssSelector("tr[class='cart-discount coupon-edgewords']"), 2, _driver);
        }

        public void DeleteItem()
        {
            DeleteButton.Click();
        }

        public void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element).Perform();

        }

    }

}