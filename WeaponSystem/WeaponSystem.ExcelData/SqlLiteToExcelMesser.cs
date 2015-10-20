namespace WeaponSystem.ExcelData
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public class SqlLiteToExcelMesser
    {
        public async Task<string> MekeMess()
        {
            //You are given a SQLite database holding more information for each product.
            //Write a program to read the MySQL database of reports, 
            //read the information from SQLite and generate a single Excel 2007 file holding some information by your choice. 
            var listForSQLite = new List<string>() { "Model", "Prise", "PriseUnits", "UnitsInStock", "Manufacturer.Name" };
            var listForExcel = new List<string>() { "Model", "Prise", "Units", "Quantity", "Manufacturer" };

            var db = new SQLite();
            var data = db.selectQuery("SELECT " + string.Join(", ", listForSQLite) + " FROM Weapons, Manufacturers AS Manufacturer WHERE ManufacturerID = Manufacturer.Id");
            //var data = db.selectQuery("SELECT * FROM Manufacturers");

            var package = ExcelFile.CreatePackage();
            ExcelFile.SetWorkbookProperties(package, "WeaponSystem", "Sample");
            var sheet = ExcelFile.CreateWorksheet(package, "Sample Sheet Name");
            ExcelFile.CreateDataRow(sheet, 1, listForExcel, true);
            ExcelFile.CreateData(sheet, 2, data);
            ExcelFile.SaveWorksheet(package, "example");

            return "SqlLite to excel Mess complete!";
        }
    }
}
