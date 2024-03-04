namespace CoreAutomation.Interfce
{
    public interface IDataHelper
    {
        public IDictionary<string, string> Data { get; set; }
        public void LoadDatabyColumn(string filePath, string spreadSheet);
    }
}
