using CoreAutomation.Interfce;

namespace CoreAutomation.Page
{
    public abstract class BasePage
    {
        protected IBrowserHelper? browserHelper;
        protected IReportHelper? reportHelper;
        protected ILoggerHelper? loggerHelper;
    }
}
