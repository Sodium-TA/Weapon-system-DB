namespace WeaponSystem.Reports
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using System.IO;
    using System.Linq;
    using Newtonsoft.Json.Linq;
    using WeaponSystem.MsSql.Data;

    public class ReportJson
    {
        private const string JsonSuccessMessage = "JSON Report created successfully.";

        public async Task<string> GenerateJsonReport()
        {
            using (WeaponSystemContext msSqlServerContext = new WeaponSystemContext())
            {
                await msSqlServerContext
                    .Weapons
                    .Select(w => new
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Manufacturer = w.Manufacturer.Name
                    })
                    .ForEachAsync(w =>
                        WriteJsonObject(w.Id.ToString(), w.Name, w.Manufacturer)
                    );
            }

            return JsonSuccessMessage;
        }

        private void WriteJsonObject(string id, string name, string manifacturer)
        {
            string reportLocation = "../../../../Generated Reports/JSON/";

            var jsonObject = new JObject(
                 new JProperty("weapon-id", id),
                 new JProperty("weapon-name", name),
                 new JProperty("manufacturer", manifacturer));

            string filePath = Path.Combine(reportLocation, string.Format("{0}.json", id));

            using (var file = File.Create(filePath))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.Write(jsonObject.ToString());
                }
            }
        }
    }
}


