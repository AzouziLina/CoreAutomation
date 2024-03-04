using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAutomation.Interfce
{
    public interface IHealeniumHelper
    {
        IWebDriver InitSelfHealingDriver(string HealeniumUri);
    }
}
