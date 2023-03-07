using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerceFinalProject.POMClasses
{
    internal class ShopPage
    {

        private IWebDriver _driver; //Field to hold a webdriver instance
        private TopNav _topNav;

        public ShopPage(IWebDriver driver, TopNav topNav) //Get the webdriver instance from the calling test
        {
            this._driver = driver;
            this._topNav = topNav;
        }

        //Locators     
        //Service Methods

        public void AddItemToCart()
        {
            _topNav.Shop.Click();
            _driver.FindElement(By.XPath("//*[@id=\"main\"]/ul/li[3]/a[2]")).Click();
        }

        public void ViewCart()
        {
            WaitForElement(By.LinkText("View cart"), 2, _driver);
            _topNav.Cart.Click();
        }


    }
}
