namespace CoreAutomation.Interfce
{
    public interface ILoggerHelper
    {
        void Info(string message);
        void Error(string message);
        void EndLogging();
    }
}
