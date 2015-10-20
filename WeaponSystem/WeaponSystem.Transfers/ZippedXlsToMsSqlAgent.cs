namespace WeaponSystem.Transfers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;

    using Readers;
    using WeaponSystem.Models;
    using WeaponSystem.MsSql.Data;

    public class ZippedXlsToMsSqlAgent
    {
        private const string MessageEnd = " from zipped .xls transfered to MsSQL!";

        public async Task<string> TransferCalibers()
        {
            KPKNameClass1.AddCalibers();

            return "Calibers" + MessageEnd;
        }

        public async Task<string> TransferWeapons(string path = "put default path here")
        {
            KPKNameClass1.AddWeapons();

            return "Weapons" + MessageEnd;

        }
    }
}
