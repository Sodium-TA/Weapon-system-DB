namespace WeaponSystem.Reports
{
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using WeaponSystem.MsSql.Data;

    public class ReportPdf
    {
        private const float FontSize = 10f;

        public void GeneratePdfReport()
        {
            string reportLocation = "../../Generated Reports/Report.pdf";

            var fs = new FileStream(reportLocation, FileMode.Create);

            var document = new Document(PageSize.A4, 25, 25, 30, 30);

            var writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            var table = new PdfPTable(5)
            {
                TotalWidth = 545f,
                LockedWidth = true
            };

            float[] widths = { 0.75f, 0.5f, 0.25f, 1.25f, 0.25f };
            table.SetWidths(widths);

            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            var cellTitle =
                new PdfPCell(new Phrase("Weapons Report", new Font(Font.FontFamily.HELVETICA, FontSize, Font.BOLD)))
                {
                    Colspan = 5,
                    HorizontalAlignment = 1,
                    PaddingBottom = 10f,
                    PaddingLeft = 10f,
                    PaddingTop = 4f
                };

            table.AddCell(cellTitle);

            var msSqlServerContext = new WeaponSystemContext();

            using (msSqlServerContext)
            {
                var weapons =
                    from weapon in msSqlServerContext.Weapons
                    select new
                    {
                        WeaponName = weapon.Name
                    };

                foreach (var weapon in weapons)
                {
                    var cellProduct =
                        new PdfPCell(
                            new Phrase(
                                weapon.WeaponName,
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
                        new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct3.PaddingBottom = 10f;
                    cellProduct3.PaddingLeft = 10f;
                    cellProduct3.PaddingTop = 4f;
                    table.AddCell(cellProduct3);

                    var cellProduct4 =
                        new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct4.PaddingBottom = 10f;
                    cellProduct4.PaddingLeft = 10f;
                    cellProduct4.PaddingTop = 4f;
                    table.AddCell(cellProduct4);

                    var cellProduct5 =
                        new PdfPCell(new Phrase(weapon.WeaponName, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
                    cellProduct5.PaddingBottom = 10f;
                    cellProduct5.PaddingLeft = 10f;
                    cellProduct5.PaddingTop = 4f;
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
    }
}