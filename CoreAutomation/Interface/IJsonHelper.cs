namespace CoreAutomation.Interfce
{
    public interface IJsonHelper
    {
        string GetStringValueFromJson(string FileName, string ValueName);
        bool GetBoolValueFromJson(string FileName, string ValueName);
    }
}
