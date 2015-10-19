namespace WeaponSystem.Transfers
{
    using System.Linq;
    using System.Threading.Tasks;
    using WeaponSystem.MongoDb.Data;
    using WeaponSystem.MsSql.Data;

    public class MongoDbToMsSqlAgent
    {
        private const string MessageEnd = " from mongoDB transfered to MsSQL!";
        private MongoDbRepository mongoRepository; 

        public MongoDbToMsSqlAgent()
        {
            this.mongoRepository = new MongoDbRepository();
        }

        public async Task<string> TransferWeaponCategories()
        {
            var weaponCategories = (await this.mongoRepository.GetWeaponCategories()).ToList();
            using (WeaponSystemContext ctx = new WeaponSystemContext())
            {
                foreach (var cat in weaponCategories)
                {
                    if (!ctx.WeaponCategoies.Any(c => c.Name == cat.Name))
                    {
                        ctx.WeaponCategoies.Add(cat);
                    }
                }

                ctx.SaveChanges();
            }

            return "Weapon Categories" + MessageEnd;
        }

        public async Task<string> TransferTargetCategories()
        {
            var targetCategories = (await this.mongoRepository.GetTargetCategories()).ToList();
            using (WeaponSystemContext ctx = new WeaponSystemContext())
            {
                foreach (var cat in targetCategories)
                {
                    if (!ctx.TargetCategories.Any(c => c.Name == cat.Name))
                    {
                        ctx.TargetCategories.Add(cat);
                    }
                }

                ctx.SaveChanges();
            }

            return "Target Categories" + MessageEnd;
        }

        public async Task<string> TransferCountries()
        {
            var countries = (await this.mongoRepository.GetCountries()).ToList();
            using (WeaponSystemContext ctx = new WeaponSystemContext())
            {
                foreach (var country in countries)
                {
                    if (!ctx.Countries.Any(c => c.Name == country.Name))
                    {
                        ctx.Countries.Add(country);
                    }
                }

                ctx.SaveChanges();
            }

            return "Countries" + MessageEnd;
        }

        public async Task<string> TransferManufacturers()
        {
            var manufacturers = (await this.mongoRepository.GetManufacturers()).ToList();
            using (WeaponSystemContext ctx = new WeaponSystemContext())
            {
                foreach (var man in manufacturers)
                {
                    if (!ctx.Manufacturers.Any(c => c.Name == man.Name))
                    {
                        ctx.Manufacturers.Add(man);
                    }
                }

                ctx.SaveChanges();
            }

            return "Manufacturers" + MessageEnd;
        }
    }
}
