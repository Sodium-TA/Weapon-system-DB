namespace Readers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.OleDb;

    public static class ExcelReader
    {
        private const string ExtractionFilePath = "../../UnZippedFiles";
        private const string RelativeFolderPath = "../../";
        public static List<List<List<string>>> GetExcelFilesAsCollection(string pathToZipFile)
        {
            FileUnziper.UnzipFile(pathToZipFile, ExtractionFilePath);

            var megaCollection = new List<List<List<string>>>();

            foreach (var file in FileUnziper.Files)
            {
                var excelContent = ExcelReader.ReadExcelFile(file);
                // Still in test mode.. all this will be extracted as method
                string xmlAsString = excelContent.GetXml();

                megaCollection.Add(XmlReader.ParseXml(xmlAsString));
            }

            return megaCollection;
        } 

        public static DataSet ReadExcelFile(string filePath)
        {
            DataSet dataSet = new DataSet();

            string connectionString = GetConnectionString(ExtractionFilePath + "/"+ filePath);

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                // Get all Sheets in Excel File
                DataTable dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Loop through all Sheets to get data
                foreach (DataRow dr in dtSheet.Rows)
                {

                    string sheetName = dr["TABLE_NAME"].ToString();

                    if (!sheetName.Contains("$"))
                    {
                        continue;
                    }

                    // Get all rows from the Sheet
                    command.CommandText = "SELECT * FROM [" + sheetName + "]";

                    DataTable dt = new DataTable();
                    dt.TableName = sheetName;

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    da.Fill(dt);

                    dataSet.Tables.Add(dt);
                }

                command = null;
                connection.Close();
            }

            return dataSet;
        }

        private static string GetConnectionString(string filePath, bool enableOldExcelReading = false)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            if (!enableOldExcelReading)
            {
                // XLSX - Excel 2007, 2010, 2012, 2013
                props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
                props["Extended Properties"] = "Excel 12.0 XML";
                props["Data Source"] = filePath;
            }
            else
            {
                // XLS - Excel 2003 and Older
                props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                props["Extended Properties"] = "Excel 8.0";
                props["Data Source"] = RelativeFolderPath + filePath;
            }

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }
    }
}
