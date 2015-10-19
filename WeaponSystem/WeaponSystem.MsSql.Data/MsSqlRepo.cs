using WeaponSystem.MsSql.Data.Migrations;

namespace WeaponSystem.MsSql.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using WeaponSystem.Models;

    public class MsSqlRepo
    {
        public async Task CreteDb()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeaponSystemContext, Configuration>());

            using (var ctx = new WeaponSystemContext())
            {
                await ctx.Weapons.ToListAsync();
                await ctx.SaveChangesAsync();
            }
        }
    }
}
