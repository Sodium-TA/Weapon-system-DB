using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponSystem.ExcelData
{
    class EntryPoint
    {
        static void Main()
        {
            //You are given a SQLite database holding more information for each product.
            //Write a program to read the MySQL database of reports, 
            //read the information from SQLite and generate a single Excel 2007 file holding some information by your choice. 

            var db = new SQLite();
            var data = db.selectQuery("SELECT * FROM Weapons");
            //var data = db.selectQuery("SELECT * FROM Manufacturers");

            var package = ExcelFile.CreatePackage();
            ExcelFile.SetWorkbookProperties(package, "WeaponSystem", "Sample");
            var sheet = ExcelFile.CreateWorksheet(package, "Sample Sheet Name");
            ExcelFile.CreateData(sheet, data);
            ExcelFile.SaveWorksheet(package, "example");
        }
    }
}
