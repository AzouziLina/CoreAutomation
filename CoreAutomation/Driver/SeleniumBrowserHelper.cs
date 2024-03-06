using CoreAutomation.Interfce;
using CoreAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Reflection;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace CoreAutomation.Driver
{
    public class SeleniumBrowserHelper : IBrowserHelper
    {
        private IWebDriver WebDriver;
        private WebDriverWait webDriverWait;
        private readonly IReportHelper reportHelper;
        private readonly ILoggerHelper loggerHelper;
        private readonly IJsonHelper JsonHelper;
        private readonly ISelfHealingHelper SelfHealingHelper;

        public SeleniumBrowserHelper(IReportHelper reporter, ILoggerHelper logger)
        {
            reportHelper = reporter;
            loggerHelper = logger;
            if (JsonHelper == null)
                JsonHelper = new JsonHelper();
            if (SelfHealingHelper == null)
                SelfHealingHelper = new HealeniumSelfHealingHelper();
        }
        private IWebDriver Driver { get { return WebDriver; } }
        private WebDriverWait Wait { get { return webDriverWait; } }

        public void Init()
        {
            try
            {
                bool HandleSelfHealing = JsonHelper.GetBoolValueFromJson("AppSettings", "selfHealing");
                if(HandleSelfHealing)
                {
                    string HealeniumUri = JsonHelper.GetStringValueFromJson("AppSettings", "healeniumUri");
                    WebDriver = SelfHealingHelper.InitSelfHealingDriver(HealeniumUri);
                }
                else
                    WebDriver  = new ChromeDriver();
                webDriverWait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(60));
                WebDriver.Manage().Window.Maximize();
                WebDriver.Navigate().GoToUrl(JsonHelper.GetStringValueFromJson("AppSettings", "baseURL"));
            }
            catch (Exception ex)
            {
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        public void Close()
        {
            if (WebDriver != null)
            {
                WebDriver.Quit();
                WebDriver.Dispose();
            }
        }
        public void ClickOnElement(By by)
        {
            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(by));
                element.Click();
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        public void ClickButtonUsingJavaScriptExecutor(By by)  
        {
            try
            {
                IWebElement buttonElement = Wait.Until(ExpectedConditions.ElementExists(by));
                IJavaScriptExecutor executor = (IJavaScriptExecutor)WebDriver;
                executor.ExecuteScript("arguments[0].click();", buttonElement);
            }
            catch (Exception ex)
            {
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        public void MouseClick(By by)   
        {
            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementExists(by));
                Actions builder = new Actions(Driver);
                builder.MoveToElement(element).Click().Perform();
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }

        public void SetTextUsingSendKeys(By by, string text)
        {
            try
            {
                IWebElement textField = Wait.Until(ExpectedConditions.ElementExists(by));
                textField.Clear();
                textField.SendKeys(text);
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        public void SetTextUsingAction(By by, string text)
        {
            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementExists(by));
                Actions builder = new Actions(Driver);
                builder.SendKeys(text).Perform();
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        public void SelectElementFromListByValue(By by,string value)
        {
            try
            {
                IWebElement dropdown = Driver.FindElement(by);
                SelectElement select = new SelectElement(dropdown);
                select.SelectByValue(value);
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        public string GetText(By by)
        {
            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementIsVisible(by));
                return element.Text;
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        public string GetAttribute(By by,string attribute)
        {
            try
            {
                IWebElement element = Wait.Until(ExpectedConditions.ElementIsVisible(by));
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        public string GetScreenshot()
        {
            if (WebDriver!=null)
            {
                var file = ((ITakesScreenshot)WebDriver).GetScreenshot();
                var img = file.AsBase64EncodedString;
                return img;
            }
            else
            return string.Empty;
        }
        public List<IWebElement> GetListofExistingElements(By by)
        {
            List<IWebElement>? WebElementlist = null;
            try
            {
                WebElementlist = Wait.Until(ExpectedConditions.ElementExists(by)).FindElements(by).ToList();
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
            return WebElementlist;
        }
        public string GetAttributeOfActiveElement(string attribute)
        {
            try
            {
                return WebDriver.SwitchTo().ActiveElement().GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                return string.Empty;
            }
        }
        public bool IsElementDisplayed(By by)
        {
            try
            {
                return Wait.Until(ExpectedConditions.ElementIsVisible(by)).Displayed;
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                return false;
            }
        }
        public bool IsElementSelected(By by)
        {
            try
            {
                return Wait.Until(ExpectedConditions.ElementIsVisible(by)).Selected;
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                return false;
            }
        }
        public bool IsElementNoLongerVisible(By by)
        {
            try
            {
                return Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                return false;
            }
        }
        public bool IsTextPresentInElementLocated(By by,string ExpectedText)
        {
            try
            {
                return Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, ExpectedText));
            }
            catch (Exception ex)
            {
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                return false;
            }
        }
    }
}
