namespace WeaponSystem.ReportsGeneratorPDF
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using WeaponSystem.MsSql.Data;

    public static class GeneratorPdf
    {
        private const float FontSize = 10f;

        private static void GeneratePdfReport()
        {
            string reportLocation = "../../Report.pdf";

            var fs = new FileStream(reportLocation, FileMode.Create);
            
            var document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            var table = new PdfPTable(5);

            table.TotalWidth = 545f;
            table.LockedWidth = true;

            float[] widths = new float[] { 0.75f, 0.5f, 0.25f, 1.25f, 0.25f };
            table.SetWidths(widths);

            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            var cellTitle = new PdfPCell(new Phrase("Weapons Report", new Font(Font.FontFamily.HELVETICA, FontSize, Font.BOLD)))
            {
                Colspan = 5,
                HorizontalAlignment = 1
            };

            cellTitle.PaddingBottom = 10f;
            cellTitle.PaddingLeft = 10f;
            cellTitle.PaddingTop = 4f;
            table.AddCell(cellTitle);

            var msSQLServerContext = new WeaponSystemContext();

            using (msSQLServerContext)
            {
                var weapons =
                    from weapon in msSQLServerContext.Weapons
                    select new
                    {
                        WeaponName = weapon.Name,
                    };


                foreach (var weapon in weapons)
                {
                    var cellProduct = new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct.PaddingBottom = 10f;
                    cellProduct.PaddingLeft = 10f;
                    cellProduct.PaddingTop = 4f;
                    table.AddCell(cellProduct);

                    var cellProduct2 = new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct.PaddingBottom = 10f;
                    cellProduct.PaddingLeft = 10f;
                    cellProduct.PaddingTop = 4f;
                    table.AddCell(cellProduct2);

                    var cellProduct3 = new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct.PaddingBottom = 10f;
                    cellProduct.PaddingLeft = 10f;
                    cellProduct.PaddingTop = 4f;
                    table.AddCell(cellProduct3);

                    var cellProduct4 = new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct.PaddingBottom = 10f;
                    cellProduct.PaddingLeft = 10f;
                    cellProduct.PaddingTop = 4f;
                    table.AddCell(cellProduct4);

                    var cellProduct5 = new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct.PaddingBottom = 10f;
                    cellProduct.PaddingLeft = 10f;
                    cellProduct.PaddingTop = 4f;
                    table.AddCell(cellProduct5);
                }

                document.Add(table);

                // Close the document
                document.Close();

                // Close the writer instance
                writer.Close();

                // Close the File Stream
                fs.Close();
            }
        }

        public static void Main()
        {
            GeneratePdfReport();
            Console.WriteLine("PDF Report successfully generated.");
        }
    }
}