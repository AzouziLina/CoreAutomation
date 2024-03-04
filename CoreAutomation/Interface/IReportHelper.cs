namespace CoreAutomation.Interfce
{
    public interface IReportHelper
    {
        void CreateTest(string testName);
        void SetTestStatusFailed(string name);
        void SetTestStatusPassed(string name);
        void SetStepStatusPassed(string msg);
        void SetStepStatusFailed(string msg);
        void EndReporting();
        void Error(string msg);
        void Info(string msg);
        void LogScreenshot(string info, string image);
        void CreateGherkinFeature(string featureName);
        void CreateGherkinScenario(string ScenarioName);
        void SetGivenStepPassed(string StepName);
        void SetWhenStepPassed(string StepName);
        void SetAndStepPassed(string StepName);
        void SetThenStepPassed(string StepName);
        void SetGivenStepFailed(string StepName, string scenarioContext);
        void SetWhenStepFailed(string StepName, string scenarioContext);
        void SetAndStepFailed(string StepName, string scenarioContext);
        void SetThenStepFailed(string StepName, string scenarioContext);
    }
}
