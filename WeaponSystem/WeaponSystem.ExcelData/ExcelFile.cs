namespace WeaponSystem.ExcelData
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Drawing;
    using System.IO;

    using OfficeOpenXml;
    using OfficeOpenXml.Style;

    public static class ExcelFile
    {
        private const string SheetName = "Price List";


        public static ExcelPackage CreatePackage()
        {
            return new ExcelPackage();
        }

        public static ExcelWorksheet CreateWorksheet(ExcelPackage excelPackage, string sheetName = SheetName)
        {
            excelPackage.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
            worksheet.Name = sheetName;

            return worksheet;
        }
        
        public static void SetWorkbookProperties(ExcelPackage excelPackage, string workbookAutor, string workbookTitle)
        {
            excelPackage.Workbook.Properties.Author = workbookAutor;
            excelPackage.Workbook.Properties.Title = workbookTitle;
        }

        public static void SaveWorksheet(ExcelPackage excelPackage, string filename, string extenstion = ".xlsx", string filePath = @"..\..\files\")
        {
            Byte[] bin = excelPackage.GetAsByteArray();
            string file = filePath + filename + extenstion;
            File.WriteAllBytes(file, bin);
           // Console.WriteLine(file + " written!");
        }


        public static void CreateDataColumn(ExcelWorksheet Worksheet, int columnIndex, string columnHeader, IEnumerable columnData)
        {
            var rowIndex = 1;
            var cell = Worksheet.Cells[rowIndex, columnIndex];
            cell.Value = columnHeader;
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            foreach (var data in columnData)
            {
                rowIndex++;
                cell = Worksheet.Cells[rowIndex, columnIndex];
                cell.Value = data;
            }
        }

        public static void CreateDataRow(ExcelWorksheet Worksheet, int rowIndex , IEnumerable rowData, bool isHeader = false)
        {
            var columnIndex = 1;
            ExcelRange cell;
            foreach (var data in rowData)
            {
                cell = Worksheet.Cells[rowIndex, columnIndex];
                cell.Value = data;
                if (isHeader)
                {
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                columnIndex++;
            }
        }


        public static void CreateData(ExcelWorksheet worksheet,  int startRowIndex, DataTable dataTable)
        {
            int rowIndex = startRowIndex;
            int colIndex = 1;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                colIndex = 1;
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    var cell = worksheet.Cells[rowIndex, colIndex];
                    cell.Value = Convert.ToString(dataRow[dataColumn.ColumnName]);
                    colIndex++;
                }

                rowIndex++;
            }
        }
    }
}
