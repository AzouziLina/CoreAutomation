using CoreAutomation.Interfce;
using CoreAutomation.Logging;
using CoreAutomation.Reporting;
using CoreAutomation.Utilities;
using CoreAutomation.Driver;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Reflection;

namespace CoreAutomation.Test
{
    public abstract class BaseTest
    {
        protected IBrowserHelper? browserHelper;
        protected IReportHelper? reportHelper;
        protected ILoggerHelper? loggerHelper;

        //test Utilities
        protected IJsonHelper? jsonHelper;

        [OneTimeSetUp]
        public void SetUpLogger()
        {
            loggerHelper = SerilogLoggerHelper.GetInstance();
        }
        [OneTimeTearDown]
        public void EndLogging()
        {
            loggerHelper.EndLogging();
        }

        [OneTimeSetUp] 
        public void SetUpReporter()
        {
            reportHelper= ExtentReportHelper.GetInstance();
        }
        [OneTimeTearDown]
        public void EndReporting()
        {
            reportHelper.EndReporting();
        }

       
        [SetUp, Order(1)]
        public void StartUpTest()
        {
            try
            {
                reportHelper.CreateTest(TestContext.CurrentContext.Test.Name);
                reportHelper.Info("Starting test");
                loggerHelper.Info("Starting Test:" + TestContext.CurrentContext.Test.Name);

                if (jsonHelper == null)
                    jsonHelper = new JsonHelper();
                if (browserHelper == null)
                {
                    string context = jsonHelper.GetStringValueFromJson("AppSettings", "DriverContext");
                    if (context == "Mobile")
                        browserHelper = new AppiumMobileHelper(reportHelper, loggerHelper);
                    else
                    {
                        browserHelper = new SeleniumBrowserHelper(reportHelper, loggerHelper);
                        reportHelper.Info("Web Application Base URL value is :" + jsonHelper.GetStringValueFromJson("AppSettings", "baseURL"));
                    }
                    reportHelper.Info("Start launching " + context + " application");
                }
                //Start test Execution
                browserHelper.Init();
            }
            catch (Exception ex)
            {
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
        }
        [TearDown] 
        public void AfterTest()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = TestContext.CurrentContext.Result.StackTrace;
                var erreurMsg = TestContext.CurrentContext.Result.Message;
                switch (status)
                {
                    case TestStatus.Failed:
                        reportHelper.SetTestStatusFailed(TestContext.CurrentContext.Test.Name);
                        //Add ScreenShot 
                        reportHelper.LogScreenshot("Ending test", browserHelper.GetScreenshot());
                        break;

                    case TestStatus.Inconclusive:
                        reportHelper.SetTestStatusFailed(TestContext.CurrentContext.Test.Name);
                        break;

                    default:
                        reportHelper.SetTestStatusPassed(TestContext.CurrentContext.Test.Name);
                        reportHelper.Info("Ending test");
                        break;
                }
                loggerHelper.Info("Ending Test:" + TestContext.CurrentContext.Test.Name);
            }
            catch (Exception ex)
            {
                reportHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                loggerHelper.Error($"{MethodBase.GetCurrentMethod().Name} crashed : Exception message is : {ex.StackTrace}");
                throw ex;
            }
            finally
            {
                browserHelper.Close();
            }
        }


    }
}
