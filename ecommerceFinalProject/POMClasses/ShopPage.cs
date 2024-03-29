﻿using TechTalk.SpecFlow.Infrastructure;

namespace ecommerceFinalProject.POMClasses
{
    internal class ShopPage
    {
        private IWebDriver _driver; //Field to hold a webdriver instance
        private readonly ISpecFlowOutputHelper _outputHelper;
        
        public ShopPage(IWebDriver driver, ISpecFlowOutputHelper outputHelper) //Get the webdriver instance from the calling test
        {
            this._driver = driver;
            _outputHelper = outputHelper;
        }

        //Service Methods
        public void AddItemToCart(string Item)
        {
            TopNav topNav = new TopNav(_driver);
            topNav.NavigateToShop();
            
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

                _outputHelper.WriteLine("Item added to cart: " + itemsInShop[randomIndex]);
                Console.WriteLine("Item added to cart: " + itemsInShop[randomIndex]);

                Item = itemsInShop[randomIndex];
            }
            _driver.FindElement(By.CssSelector("a[aria-label='Add “" + Item + "” to your cart']")).Click();
        }

        public void ViewCart()
        {
            TopNav topNav = new TopNav(_driver);
            WaitForElement(By.LinkText("View cart"), 2, _driver);
            topNav.NavigateToCart();
        }
    }
}