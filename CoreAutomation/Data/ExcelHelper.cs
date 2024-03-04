using CoreAutomation.Interfce;
using System.Data;
using ExcelDataReader;

namespace CoreAutomation.Data
{
    public class ExcelHelper : IDataHelper
    {
        public ExcelHelper()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
        public IList<string> Methods { get; set; }= new List<string>();

        public void LoadDatabyColumn(string filePath, string spreadSheet)
        {
            if(File.Exists(filePath)) 
            {  
                using (var stream =File.Open(filePath,FileMode.Open,FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = true,
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true, }
                        });

                        //Create data list from spreadsheet by Column Name
                        DataTable table = result.Tables[spreadSheet];// The result of each spreadsheet is in result.tables
                        {
                            if (table != null)
                            {
                                foreach(DataColumn column in table.Columns) 
                                {
                                    Data.Add(column.ColumnName, table.Rows[0][table.Columns.IndexOf(column.ColumnName)].ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
