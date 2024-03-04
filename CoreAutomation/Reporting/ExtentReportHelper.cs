using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using CoreAutomation.Interfce;
using System.Reflection;

namespace CoreAutomation.Reporting
{
    public class ExtentReportHelper :IReportHelper
    {
        public ExtentReports Extent { get; set; }
        public ExtentSparkReporter Reporter { get; set; }
        public ExtentTest Test { get; set; }
        public ExtentTest Scenario { get; set; }

        public static ExtentReportHelper extentReportHelper;
        public static ExtentReportHelper GetInstance()
        {
            if (extentReportHelper == null)
                extentReportHelper = new ExtentReportHelper();
            return extentReportHelper;
        }

        public ExtentReportHelper()   //start reporting
        {
            Extent = new ExtentReports();
            Reporter = new ExtentSparkReporter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $".\\Result\\Report.html");
            Extent.AttachReporter(Reporter);
            Reporter.Config.DocumentTitle = "Automation Testing Report";
            Reporter.Config.ReportName = "Functional Test Automation";
            Reporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;
            Extent.AddSystemInfo("Application Under Test", "E-Commerce Application");
            Extent.AddSystemInfo("Machine", Environment.MachineName);
            Extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }
        public void CreateTest(string testName)
        {
           Test = Extent.CreateTest(testName);
        }
        public void SetTestStatusPassed(string testName)
        {
            Test.Pass("Test Executed succesfuly: "+ testName);
        }
        public void SetTestStatusFailed(string testName)
        {
            Test.Fail("Test not Executed succesfuly: " + testName);
        }
        public void SetStepStatusPassed(string msg)
        {
            Test.Pass(msg);
        }
        public void SetStepStatusFailed(string msg)
        {
            Test.Fail(msg);
        }
        public void EndReporting()
        {
            Extent.Flush();
        }
        public void Error(string msg)
        {
            Test.Log(Status.Error, msg);
        }
        public void Info(string msg)
        {
            Test.Log(Status.Info, msg);
        }
        public void LogScreenshot(string info, string image)
        {
            Test.Info(info,MediaEntityBuilder.CreateScreenCaptureFromBase64String(image).Build());
        }
        //Gherkin Reporting
        public void CreateGherkinFeature(string featureName)
        {
            Test = Extent.CreateTest<Feature>(featureName);
        }
        public void CreateGherkinScenario(string ScenarioName)
        {
            Scenario = Test.CreateNode<Scenario>(ScenarioName);
        }
        public void SetGivenStepPassed(string StepName)
        {
            Scenario.CreateNode<Given>(StepName);
        }
        public void SetWhenStepPassed(string StepName)
        {
            Scenario.CreateNode<When>(StepName);
        }
        public void SetAndStepPassed(string StepName)
        {
            Scenario.CreateNode<And>(StepName);
        }
        public void SetThenStepPassed(string StepName)
        {
            Scenario.CreateNode<Then>(StepName);
        }
        public void SetGivenStepFailed(string StepName, string scenarioContext)
        {
            Scenario.CreateNode<Given>(StepName).Fail(scenarioContext);
        }
        public void SetWhenStepFailed(string StepName, string scenarioContext)
        {
            Scenario.CreateNode<When>(StepName).Fail(scenarioContext);
        }
        public void SetAndStepFailed(string StepName, string scenarioContext)
        {
            Scenario.CreateNode<And>(StepName).Fail(scenarioContext);
        }
        public void SetThenStepFailed(string StepName, string scenarioContext)
        {
            Scenario.CreateNode<Then>(StepName).Fail(scenarioContext);
        }
    }
}
