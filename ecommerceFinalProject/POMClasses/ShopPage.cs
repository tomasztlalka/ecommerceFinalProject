using OpenQA.Selenium.DevTools.V108.Page;

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


        //Service Methods

        public void AddItemToCart()
        {
            _topNav.NavigateToShop();
            string Item = TestContext.Parameters["item"];

            //TODO: write a "random" item case too
            if (Item == "random" || Item == null)
            {
                string[] ItemsOfClothing = new string[6];

                
                //List<IWebElement> ItemsTest = new List<IWebElement>();
                //_driver.FindElements(By.CssSelector("\"a[aria-label='Add “"));


                ItemsOfClothing[0] = "Beanie";
                ItemsOfClothing[1] = "Belt";
                ItemsOfClothing[2] = "Cap";
                ItemsOfClothing[3] = "Hoodie";
                ItemsOfClothing[4] = "Hoodie with Logo";
                ItemsOfClothing[5] = "Hoodie with Pocket";
                

                Random rand = new Random();
                var randomIndex = rand.Next(0, ItemsOfClothing.Length);
                Console.WriteLine(ItemsOfClothing[randomIndex]);

                Item = ItemsOfClothing[randomIndex];
            }
            

            _driver.FindElement(By.CssSelector("a[aria-label='Add “" + Item + "” to your cart']")).Click();
            
        }

        public void ViewCart()
        {
            WaitForElement(By.LinkText("View cart"), 2, _driver);
            _topNav.NavigateToCart();
        }


    }

}