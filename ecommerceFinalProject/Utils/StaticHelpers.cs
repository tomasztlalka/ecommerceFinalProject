using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;


namespace ecommerceFinalProject.Utils
{
    internal static class StaticHelpers
    {
        //Static classes dont need to be isntantiated before use
        //However member methods will need to be passed the driver to use
        //As there is no constructor to set a field
        public static void WaitForElement(By locator, int timeToWaitInSeconds, IWebDriver driver)
        {
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWaitInSeconds));
            wait2.Until(drv => drv.FindElement(locator).Displayed);
        }

        

        public static void TakeScreenshotOfElement(string locator, string filename)
        {
            IWebElement form = TestBaseSpecflow.driver.FindElement(By.CssSelector(locator));
            ITakesScreenshot formss = form as ITakesScreenshot;
            var screenshotForm = formss.GetScreenshot();
            screenshotForm.SaveAsFile(@"C:\screenshots\" + filename + ".png", ScreenshotImageFormat.Png);            
        }


        //A method for scrolling to a specific web element
        public static void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(TestBaseSpecflow.driver);
            actions.MoveToElement(element).Perform();

        }

        
        // Scrolls to the page's bottom using javascript
        public static void ScrollToBottom()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)TestBaseSpecflow.driver;
            string title = (string)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            
        }


    }
}
