namespace WeaponSystem.Reports
{
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using iTextSharp.text.html;
    using iTextSharp.text.pdf;
    using WeaponSystem.Models;
    using WeaponSystem.MsSql.Data;

    public class ReportPdf
    {
        private const float FontSize = 10f;
        private const string PdfSuccessMessage = "PDF Report created successfully.";

        public string GeneratePdfReport()
        {
            string reportLocation = "../../../../Generated Reports/PDF/Report.pdf";

            var fs = new FileStream(reportLocation, FileMode.Create);

            var document = new Document(PageSize.A4, 25, 25, 30, 30);

            var writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            var table = new PdfPTable(3)
            {
                TotalWidth = 545f,
                LockedWidth = true
            };

            float[] widths = { 0.5f, 0.75f, 0.75f };
            table.SetWidths(widths);

            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            var cellTitle =
                new PdfPCell(new Phrase("FULL LIST OF WEAPONS", new Font(Font.FontFamily.HELVETICA, FontSize + 20, Font.BOLD)))
                {
                    Colspan = 3,
                    HorizontalAlignment = 1,
                    PaddingBottom = 14f,
                    PaddingLeft = 10f,
                    PaddingTop = 14f
                };

            table.AddCell(cellTitle);

            var cellTitle2 =
                new PdfPCell(new Phrase("Weapon Id", new Font(Font.FontFamily.HELVETICA, FontSize + 10, Font.BOLD)))
                {
                    PaddingBottom = 10f,
                    PaddingLeft = 10f,
                    PaddingTop = 4f
                };

            table.AddCell(cellTitle2);

            var cellTitle3 =
                new PdfPCell(new Phrase("Weapon Name", new Font(Font.FontFamily.HELVETICA, FontSize + 10, Font.BOLD)))
                {
                    PaddingBottom = 10f,
                    PaddingLeft = 10f,
                    PaddingTop = 4f
                };

            table.AddCell(cellTitle3);

            var cellTitle4 =
                new PdfPCell(new Phrase("Manufacturer", new Font(Font.FontFamily.HELVETICA, FontSize + 10, Font.BOLD)))
                {
                    PaddingBottom = 10f,
                    PaddingLeft = 10f,
                    PaddingTop = 4f
                };

            table.AddCell(cellTitle4);

            var msSqlServerContext = new WeaponSystemContext();

            using (msSqlServerContext)
            {
                var weaponsInfo =
                    from weapon in msSqlServerContext.Weapons
                    join manufacturer in msSqlServerContext.Manufacturers
                    on weapon.ManufacturerId equals manufacturer.Id
                    select new
                    {
                        WeaponId = weapon.Id,
                        WeaponName = weapon.Name,
                        Manufacturer = manufacturer.Name
                    };

                foreach (var weapon in weaponsInfo)
                {
                    var cellProduct =
                        new PdfPCell(
                            new Phrase(
                                weapon.WeaponId.ToString(),
                                new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)))
                        {
                            PaddingBottom = 10f,
                            PaddingLeft = 10f,
                            PaddingTop = 4f
                        };
                    table.AddCell(cellProduct);

                    var cellProduct2 =
                        new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct2.PaddingBottom = 10f;
                    cellProduct2.PaddingLeft = 10f;
                    cellProduct2.PaddingTop = 4f;
                    table.AddCell(cellProduct2);

                    var cellProduct3 =
                        new PdfPCell(new Phrase(weapon.Manufacturer, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct3.PaddingBottom = 10f;
                    cellProduct3.PaddingLeft = 10f;
                    cellProduct3.PaddingTop = 4f;
                    table.AddCell(cellProduct3);
                }

                document.Add(table);

                // Close the document
                document.Close();

                // Close the writer instance
                writer.Close();

                // Close the File Stream
                fs.Close();
            }

            return PdfSuccessMessage;
        }
    }
}