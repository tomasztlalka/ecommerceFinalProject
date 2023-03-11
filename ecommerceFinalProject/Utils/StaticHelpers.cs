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

        

        public static void TakeScreenshotOfElement(IWebDriver driver, By locator, string filename)
        {
            IWebElement form = driver.FindElement(locator);
            ITakesScreenshot formss = form as ITakesScreenshot;
            var screenshotForm = formss.GetScreenshot();
            screenshotForm.SaveAsFile(@"C:\screenshots\" + filename, ScreenshotImageFormat.Png);            
        }


        //A method for scrolling to a specific web element
        public static void ScrollToElement(IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();

        }
    }
}
