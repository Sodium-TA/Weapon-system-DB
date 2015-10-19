namespace WeaponSystem.Transfers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    using Readers;
    using WeaponSystem.Models;
    using WeaponSystem.MsSql.Data;

    public class ZippedXlsToMsSqlAgent
    {
        private const string MessageEnd = " from zipped .xls transfered to MsSQL!";
        public async Task<string> TransferWeapons(string path = "put default path here")
        {
            // await before the time cost operation 
            /* 
            example:     var weaponCategories = (await WeaponCategoriesCollection.Find(new BsonDocument()).ToListAsync())
                                                    .Select(bs => BsonSerializer.Deserialize<WeaponCategory>(bs)).ToList();
                */

            using (WeaponSystemContext db = new WeaponSystemContext())
            {
                var i = 0;
                var megaCollection = ExcelReader.GetExcelFilesAsCollection();
                var weaponsCat = db.WeaponCategoies.Find(1);

                foreach (var collection in megaCollection)
                {
                    foreach (var weapon in collection)
                    {
                        var weapons = new Weapon();

                        weapons.Name = weapon[0];
                        //weapons.Id = i;
                        weapons.Manufacturer = null;
                        weapons.Description = null;
                        weapons.RelaseYear = 0;
                        weapons.WeaponCategory = weaponsCat;
                        weapons.ManufacturerId = null;
                        weapons.WeaponType = WeaponType.CloseRange;
                        weapons.Targets = null;
                        db.Weapons.Add(weapons);
                    }

                    i++;
                }

                db.SaveChanges();
            }

            return "Weapons" + MessageEnd;
        }
    }
}
