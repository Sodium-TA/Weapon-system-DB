namespace WeaponSystem.Transfers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

            return "Weapons" + MessageEnd;
        }
    }
}
