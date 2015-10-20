namespace WeaponSystem.Reports
{
    using System;

    public static class EntryPoint
    {
        private const string PdfSuccessMessage = "PDF Report created successfully.";
        private const string XmlSuccessMessage = "XML Report created successfully.";
        private const string JsonSuccessMessage = "JSON Report created successfully.";

        public static void Main()
        {
            // PDF
            var pdfReport = new ReportPdf();
            pdfReport.GeneratePdfReport();
            Console.WriteLine(PdfSuccessMessage);

/*            // XML
            var xmlReport = new ReportXml();
            xmlReport.GenerateXmlReport();
            Console.WriteLine(XmlSuccessMessage);

            // JSON
            var jsonReport = new ReportJson();
            jsonReport.GenerateJsonReport();
            Console.WriteLine(JsonSuccessMessage);*/
        }
    }
}