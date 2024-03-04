using CoreAutomation.Interfce;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreAutomation.Utilities
{
    public class JsonHelper : IJsonHelper
    {
        public string GetStringValueFromJson(string FileName, string ValueName)
        {
            //FileName="appsettings"
            //ValueName="BaseURL"
            string? JsonValue;
            try
            {
                JsonValue = new ConfigurationBuilder()
              .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName)
              .AddJsonFile(FileName + ".json", true, true).Build().GetSection(FileName + ":" + ValueName)?.Value;

            }
            catch (Exception ex)
            {
                throw new Exception($"Exception msg is {ex.StackTrace} ");
            }
            return JsonValue;
        }
        public bool GetBoolValueFromJson(string FileName, string ValueName)
        {
            bool? JsonValue;
            try
            {
                JsonValue = new ConfigurationBuilder()
              .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName)
              .AddJsonFile(FileName + ".json", true, true).Build().GetSection(FileName + ":" + ValueName)?.Get<bool?>();

            }
            catch (Exception ex)
            {
                throw new Exception($"Exception msg is {ex.StackTrace} ");
            }
            return (bool)JsonValue;
        }
    }
}
