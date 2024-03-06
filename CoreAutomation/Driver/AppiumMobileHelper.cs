using CoreAutomation.Interfce;
using CoreAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Appium.Android;
using System.Reflection;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace CoreAutomation.Driver

{
    public class AppiumMobileHelper:IBrowserHelper
    {
        private AndroidDriver MobileDriver;
        private readonly IReportHelper reportHelper;
        private readonly ILoggerHelper loggerHelper;
        private readonly IJsonHelper JsonHelper;
        private WebDriverWait WebDriverWait;
        public AppiumMobileHelper(IReportHelper reporter, ILoggerHelper logger)
        {
            reportHelper = reporter;
            loggerHelper = logger;
            if (JsonHelper == null)
                JsonHelper = new JsonHelper();
        }

        private AndroidDriver Driver { get { return MobileDriver; } }
        private WebDriverWait Wait { get { return WebDriverWait; } }

        public void Init()
        {
            try 
            {
                string AppiumUri = JsonHelper.GetStringValueFromJson("AppSettings", "AppiumUri");
                var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? AppiumUri);
                var driverOptions = new AppiumOptions()
                {
                    PlatformName = JsonHelper.GetStringValueFromJson("AppSettings", "PlatformName"),
                    DeviceName = JsonHelper.GetStringValueFromJson("AppSettings", "DeviceName"),
                    App = JsonHelper.GetStringValueFromJson("AppSettings", "App"),
                    AutomationName = AutomationName.AndroidUIAutomator2,// to be updated for IOS also
                };
                MobileDriver = new AndroidDriver(serverUri, driverOptions, TimeSpan.FromSeconds(180));
                WebDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            }
            catch (Exception ex)
            {
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
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
        public void Close()
        {
            if (MobileDriver != null)
            {
                MobileDriver.Quit();
                MobileDriver.Dispose();
            }
        }

        public string GetAttribute(By by, string attribute)
        {
            throw new NotImplementedException();
        }

        public string GetAttributeOfActiveElement(string attribute)
        {
            throw new NotImplementedException();
        }

        public List<IWebElement> GetListofExistingElements(By by)
        {
            throw new NotImplementedException();
        }

        public string GetScreenshot()
        {
            throw new NotImplementedException();
        }

        public string GetText(By by)
        {
            throw new NotImplementedException();
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

        public bool IsElementNoLongerVisible(By by)
        {
            throw new NotImplementedException();
        }

        public bool IsElementSelected(By by)
        {
            throw new NotImplementedException();
        }

        public bool IsTextPresentInElementLocated(By by, string ExpectedText)
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

        public void MouseClick(By by)
        {
            throw new NotImplementedException();
        }

        public void SelectElementFromListByValue(By by, string value)
        {
            throw new NotImplementedException();
        }
        public void ClickButtonUsingJavaScriptExecutor(By by)
        {
            throw new NotImplementedException();
        }

    }
} 

