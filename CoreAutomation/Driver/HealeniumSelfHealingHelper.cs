using CoreAutomation.Interfce;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace CoreAutomation.Driver
{
    public class HealeniumSelfHealingHelper : IHealeniumHelper
    {
        public HealeniumSelfHealingHelper()
        {

        }
        public IWebDriver InitSelfHealingDriver(string HealeniumUri)
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
    }
}
