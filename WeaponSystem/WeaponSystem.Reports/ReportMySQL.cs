namespace WeaponSystem.Reports
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;
    using Weapons.MySqlDb.Actions;
    using Weapons.MySqlDb.Data;
    using WeaponSystem.MsSql.Data;

    public class ReportMySQL
    {
        private const string MySQLSuccessMessage = "MySQL Reports created successfully.";

        public async Task<string> GenerateMySQLReport()
        {

            var list = new List<JObject>();
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
                        list.Add(CreateJObject(w.Id, w.Name, w.Manufacturer))
                    );
            }

            using (WeaponReportsDbContext MySqlServerContext = new WeaponReportsDbContext())
            {
                MySqlActions.AddReports(list, MySqlServerContext);
            }

            return MySQLSuccessMessage;
        }

        private JObject CreateJObject(int id, string name, string manufacturer)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "[null]";
            }
            if (string.IsNullOrEmpty(manufacturer))
            {
                manufacturer = "[null]";
            }
            var jsonObject = new JObject(
                 new JProperty("WeaponID", id.ToString()),
                 new JProperty("WeaponName", name),
                 new JProperty("Manufacturer", manufacturer));
            return jsonObject;
        }
    }
}
