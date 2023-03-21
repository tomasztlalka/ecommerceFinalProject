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

        public static void TakeScreenshotOfElement(string locator, string filename, bool wait=false)
        {
            if (wait)
            {
                //Need to wait for element to be in sight
                Thread.Sleep(1000);
            }

            //Setting the path for screenshots relative to project directory
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            string pathToRemove = "bin\\Debug\\net6.0\\";
            //Replacing the last part of directory so that it points to the screenshots folder
            startupPath = startupPath.Replace(pathToRemove, "Screenshots\\");


            IWebElement form = TestBaseSpecflow.driver.FindElement(By.CssSelector(locator));
            ITakesScreenshot formss = form as ITakesScreenshot;
            var screenshotForm = formss.GetScreenshot();
            screenshotForm.SaveAsFile(startupPath + filename + ".png", ScreenshotImageFormat.Png);

            //Adding file to results
            TestContext.AddTestAttachment(startupPath + filename + ".png", filename);
        }


        //A method for scrolling to a specific web element
        public static void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(TestBaseSpecflow.driver);
            actions.MoveToElement(element).Perform();

        }

        public static string CheckNull(string dataValue)
        {
            if (dataValue == null)
            {
                throw new ArgumentNullException(nameof(dataValue));
            }
            return dataValue;
        }

        public static string GetContextParameter(string parameter)
        {
            
            return CheckNull(TestContext.Parameters[parameter]);
        }
    }
}
