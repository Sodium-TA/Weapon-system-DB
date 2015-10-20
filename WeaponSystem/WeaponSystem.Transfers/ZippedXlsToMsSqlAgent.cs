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

        public async Task<string> TransferWeapons(string path = "put default path here")
        {
            KPKNameClass1.AddWeapons();

            return "Weapons" + MessageEnd;

        }

        private static Caliber GetCaliber(List<Caliber> calibersCollection, string caliberSize)
        {
            foreach (var caliber in calibersCollection)
            {
                if (caliber.Name == caliberSize)
                {
                    return caliber;
                }
            }

            return null;
        }

        private static WeaponCategory GetCategory(List<WeaponCategory> categoryCollection, string weaponCategoryName)
        {
            foreach (var category in categoryCollection)
            {
                if (category.Name == weaponCategoryName)
                {
                    return category;
                }
            }

            return null;
        }

        private static Manufacturer GetManufacturer(List<Manufacturer> manufacturerCollection, string manufacturerName)
        {
            foreach (var manufacturer in manufacturerCollection)
            {
                if (manufacturer.Name == manufacturerName)
                {
                    return manufacturer;
                }
            }

            return null;
        }
    }
}
