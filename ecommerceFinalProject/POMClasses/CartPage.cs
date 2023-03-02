using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       
        public IWebElement deleteButton => _driver.FindElement(By.CssSelector("#post-5 > div > div > form > table > tbody > tr.woocommerce-cart-form__cart-item.cart_item > td.product-remove > a"));

        public IWebElement couponField => _driver.FindElement(By.Name("coupon_code"));

        public IWebElement applyButton => _driver.FindElement(By.Name("apply_coupon"));

        public IWebElement subTotal => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span"));

        public IWebElement cartTotal => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.order-total > td > strong > span > bdi"));
        public string appliedCouponFieldPath=> "#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > th";

        //Service Methods

        public void EnterCouponCode()
        {
            couponField.SendKeys(TestContext.Parameters["edgewords_coupon"]);
            applyButton.Click();
        }

        public void DeleteItem()
        {
            deleteButton.Click();
        }

          
    }
}
