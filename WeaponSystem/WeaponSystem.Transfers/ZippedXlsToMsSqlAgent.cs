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
            using (WeaponSystemContext db = new WeaponSystemContext())
            {
                var i = 0;
                var megaCollection = ExcelReader.GetExcelFilesAsCollection("../../../../Weapons Source Data/w.zip");
                var weaponsCat = db.WeaponCategoies.ToList();
                var manufacturers = db.Manufacturers.ToList();

                foreach (var collection in megaCollection)
                {
                    foreach (var weaponItem in collection)
                    {
                        var weapon = new Weapon();

                        weapon.Name = weaponItem[1];
                        weapon.Manufacturer = GetManufacturer(manufacturers, weaponItem[2]);
                        weapon.Description = null;
                        weapon.RelaseYear = 0;
                        weapon.WeaponCategory = GetCategory(weaponsCat, weaponItem[0]);
                        weapon.ManufacturerId = null;
                        weapon.WeaponType = WeaponType.CloseRange;
                        weapon.ImageUrl = weaponItem[4];
                        weapon.Targets = null;

                        if (!db.Weapons.Any(w => w.Name == weapon.Name))
                        {
                            db.Weapons.Add(weapon);
                        }

                        Console.WriteLine(weapon.Name);
                    }

                    i++;
                }

                db.SaveChanges();
            }

            return "Weapons" + MessageEnd;

        }

        private WeaponCategory GetCategory(List<WeaponCategory> categoryCollection, string weaponCategoryName)
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

        private Manufacturer GetManufacturer(List<Manufacturer> manufacturerCollection, string manufacturerName)
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
