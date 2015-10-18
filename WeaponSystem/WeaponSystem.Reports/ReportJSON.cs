namespace WeaponSystem.Reports
{
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json.Linq;
    using WeaponSystem.MsSql.Data;

    public class ReportJson
    {
        public void GenerateJsonReport()
        {
            var msSqlServerContext = new WeaponSystemContext();

            using (msSqlServerContext)
            {
                var weaponsReport =
                    from weapon in msSqlServerContext.Weapons
                    join manufacturer in msSqlServerContext.Manufacturers
                        on weapon.ManufacturerId equals manufacturer.Id
                    select new
                    {
                        WeaponId = weapon.Id,
                        WeaponName = weapon.Name,
                        Manufacturer = manufacturer.Name
                    };

                foreach (var weapon in weaponsReport)
                {
                    var jsonObject = new JObject(
                        new JProperty("weapon-id", weapon.WeaponId),
                        new JProperty("weapon-name", weapon.WeaponName),
                        new JProperty("manufacturer", weapon.Manufacturer));

                    string reportLocation = "../../Generated Reports/";
                    string filePath = Path.Combine(reportLocation, string.Format("{0}.json", weapon.WeaponId));

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
    }
}