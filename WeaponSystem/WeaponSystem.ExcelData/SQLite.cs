namespace WeaponSystem.ExcelData
{
    using System;
    using System.Data;
    using System.Data.SQLite;

    public class SQLite
    {
        private SQLiteConnection sqlite;

        public SQLite()
        {
            sqlite = new SQLiteConnection("Data Source=../../files/ManufacturersPrices.sqlite3");
        }

        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter adapter;
            DataTable dataTable = new DataTable();

            try
            {
                SQLiteCommand cmd;
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;
                sqlite.Open();
                adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dataTable); 
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
            }

            sqlite.Close();
            return dataTable;
        }
    }
}
