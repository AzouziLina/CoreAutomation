using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace CoreAutomation.Interfce
{
    public interface ISelfHealingHelper
    {
        IWebDriver InitSelenoidSelfHealingDriver(string HealeniumUri);
        AndroidDriver InitAppiumSelfHealingDriver(string HealeniumUri);
    }
}
