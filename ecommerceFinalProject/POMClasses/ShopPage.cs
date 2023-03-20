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

        public void AddItemToCart(string Item)
        {
           _topNav.NavigateToShop();
            
            //If the 'Item' parameter is null or set to 'random', the item will be selected randomly
            if (Item.ToLower() == "random" || Item == null)
            {
                List<string> itemsInShop = new List<string>();
                string itemAttribute;

                foreach (var el in _driver.FindElements(By.CssSelector("a[class='button product_type_simple add_to_cart_button ajax_add_to_cart']")))
                {
                    itemAttribute = el.GetAttribute("aria-label");
                    itemAttribute = itemAttribute[5..^14];
                    itemsInShop.Add(itemAttribute);
                }

                Random rand = new Random();
                var randomIndex = rand.Next(0, itemsInShop.Count);
                Console.WriteLine("Item added to cart: " + itemsInShop[randomIndex]);

                Item = itemsInShop[randomIndex];
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