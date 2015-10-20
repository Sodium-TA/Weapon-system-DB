using System.Data.Entity;
using System.Threading.Tasks;

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

        public async Task<string> GeneratePdfReport()
        {
            string reportLocation = "../../../../Generated Reports/PDF/Report.pdf";

            var fs = new FileStream(reportLocation, FileMode.Create);

            var document = new Document(PageSize.A4, 25, 25, 30, 30);

            var writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            var table = new PdfPTable(4)
            {
                TotalWidth = 545f,
                LockedWidth = true
            };

            float[] widths = { 1f, 0.75f, 0.75f, 0.75f };
            table.SetWidths(widths);

            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            var header =
                new PdfPCell(new Phrase("WEAPONS CHEATSHEET", new Font(Font.FontFamily.HELVETICA, FontSize + 20, Font.BOLD)))
                {
                    Colspan = 4,
                    HorizontalAlignment = 1,
                    PaddingBottom = 14f,
                    PaddingTop = 14f
                };

            table.AddCell(header);

            var cellTitle1 =
                new PdfPCell(new Phrase("Thumb", new Font(Font.FontFamily.HELVETICA, FontSize + 5, Font.BOLD)))
                {
                    Padding = 4f
                };

            table.AddCell(cellTitle1);

            var cellTitle2 =
                new PdfPCell(new Phrase("Weapon Name", new Font(Font.FontFamily.HELVETICA, FontSize + 5, Font.BOLD)))
                {
                    PaddingBottom = 10f,
                    PaddingLeft = 10f,
                    PaddingTop = 4f
                };

            table.AddCell(cellTitle2);

            var cellTitle3 =
                new PdfPCell(new Phrase("Manufacturer", new Font(Font.FontFamily.HELVETICA, FontSize + 5, Font.BOLD)))
                {
                    PaddingBottom = 10f,
                    PaddingLeft = 10f,
                    PaddingTop = 4f
                };

            table.AddCell(cellTitle3);

            var cellTitle4 =
                new PdfPCell(new Phrase("Category", new Font(Font.FontFamily.HELVETICA, FontSize + 5, Font.BOLD)))
                {
                    PaddingBottom = 10f,
                    PaddingLeft = 10f,
                    PaddingTop = 4f
                };

            table.AddCell(cellTitle4);

           // var msSqlServerContext = new WeaponSystemContext();

            using (WeaponSystemContext msSqlServerContext = new WeaponSystemContext())
            {
                await msSqlServerContext
                    .Weapons
                    .Select(w => new
                    {
                        Name = w.Name,
                        Manufacturer = w.Manufacturer.Name,
                        Picrire = w.ImageUrl,
                        Category = w.WeaponCategory.Name
                    })
                    .ForEachAsync(w =>
                        FillPdfTable(table, w.Name, w.Category, w.Manufacturer, w.Picrire)
                    );
            }
          

                document.Add(table);

                // Close the document
                document.Close();

                // Close the writer instance
                writer.Close();

                // Close the File Stream
                fs.Close();
            

            return PdfSuccessMessage;
        }

        private void FillPdfTable(PdfPTable table, string name, string category, string manifacturer, string picture)
        {
            var cellProduct1 =
                       new PdfPCell(Image.GetInstance(picture), true);
            cellProduct1.PaddingBottom = 10f;
            cellProduct1.PaddingLeft = 10f;
            cellProduct1.PaddingTop = 4f;
            table.AddCell(cellProduct1);

            var cellProduct2 =
                new PdfPCell(new Phrase(name, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
            cellProduct2.PaddingBottom = 10f;
            cellProduct2.PaddingLeft = 10f;
            cellProduct2.PaddingTop = 4f;
            table.AddCell(cellProduct2);

            var cellProduct3 =
                new PdfPCell(new Phrase(manifacturer, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
            cellProduct3.PaddingBottom = 10f;
            cellProduct3.PaddingLeft = 10f;
            cellProduct3.PaddingTop = 4f;
            table.AddCell(cellProduct3);

            var cellProduct4 =
                new PdfPCell(new Phrase(category, new Font(Font.FontFamily.HELVETICA, FontSize, Font.NORMAL)));
            cellProduct4.PaddingBottom = 10f;
            cellProduct4.PaddingLeft = 10f;
            cellProduct4.PaddingTop = 4f;
            table.AddCell(cellProduct4);
        }
    }
}