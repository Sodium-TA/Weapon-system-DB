using Readers;
using WeaponSystem.Models;
using WeaponSystem.MongoDb.Data;
using WeaponSystem.MsSql.Data;

namespace WeaponSystem.Transfers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class XmlToMsSqlAndMongoDbAgent
    {
        private const string DataPath = "../../../../Weapons Source Data/targets.xml";

        public async Task<string> GetXmlData()
        {
            var targets = XmlReader.ReadXmlCollectionFromFile(DataPath);

            var repo = new MongoDbRepository();

            await repo.InsertTargets(targets);

            using (WeaponSystemContext db = new WeaponSystemContext())
            {
                var targetCats = db.TargetCategories.ToList();

                foreach (var targetItem in targets)
                {
                    if (!targetCats.Any(c  => c.Name == targetItem[0]))
                    {
                        var newTargetCat = new Target()
                        {
                            Name = targetItem[0]
                        };
                    }

                    var target = new Target();
                    var targetItemName = targetItem[1];

                    var id = db.TargetCategories.
                        Where(c => c.Name == targetItemName)
                        .FirstOrDefault();
                  
                    if (id == null && !db.Targets.Any(t => t.Name == target.Name))
                    {
                        target.Name = targetItem[0];
                        target.TargetCategoryId = id.Id;

                        db.Targets.Add(target);
                    }
                }

                db.SaveChanges();
            }

            return "Targets from xml dispatched";
        }
    }
}
