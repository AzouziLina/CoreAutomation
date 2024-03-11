using CoreAutomation.Interfce;
using CoreAutomation.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace CoreAutomation.Driver
{
    public class HealeniumSelfHealingHelper : ISelfHealingHelper
    {
        public HealeniumSelfHealingHelper()
        {

        }
        public IWebDriver InitSelenoidSelfHealingDriver(string HealeniumUri)
        {
            try
            {
                var optionsChrome = new ChromeOptions();
                optionsChrome.AddArguments("--no-sandbox");

                /* Set Selenoid UI Options */
                optionsChrome.AddAdditionalOption("selenoid:options", new Dictionary<string, object>
                {
                    /* Add test Execution name */
                    ["name"] = "Runtime Test Execution...",

                    /* Enable test execution view */
                    ["enableVNC"] = true,

                    /* Enable video recording */
                    ["enableVideo"] = true
                });
                IWebDriver SelfHealingDriver = new RemoteWebDriver(new Uri(HealeniumUri), optionsChrome);
                return SelfHealingDriver;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AndroidDriver InitAppiumSelfHealingDriver(string HealeniumUri)
        {
            try
            {
                var driverOptions = new AppiumOptions()
                {
                    PlatformName = "Android",
                    DeviceName = "Pixel 3a API 30",
                    App = "C:\\Users\\LinaAZOUZI\\Desktop\\ubuy.apk",
                    AutomationName = AutomationName.AndroidUIAutomator2,// to be updated for IOS also
                };
                var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? HealeniumUri);
                AndroidDriver MobileDriver = new AndroidDriver(serverUri, driverOptions, TimeSpan.FromSeconds(180));
                return MobileDriver;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
