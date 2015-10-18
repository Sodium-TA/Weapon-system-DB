namespace WeaponSystem.TestConsoleClient
{
    using System;
    using System.Data;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using MongoDb.Data;
    using MsSql.Data;

    public class MondoDbHandler
    {
        public async void GetMongoData()
        {

            Console.WriteLine("бефоре traj");
            try
            {

                var repo = new MongoDbRepository();

                var categories = (await repo.GetWeaponCategories()).ToList();

                var ctx = new WeaponSystemContext();

                Console.WriteLine("бефоре усинг");

                using (ctx)
                {
                    Console.WriteLine("ин усинг");

                    foreach (var category in categories)
                    {
                        if (!ctx.WeaponCategoies.Any(ct => ct.Name == category.Name))
                        {
                            ctx.WeaponCategoies.Add(category);
                            Console.WriteLine(category.Name);
                        }
                    }

                    ctx.SaveChanges();
                }

                Console.WriteLine("Caregories inserted");


            }
            catch (DataException ex)
            {
                Console.WriteLine("Caregories feid  " + ex.Message);
            }
        }
    }
}
