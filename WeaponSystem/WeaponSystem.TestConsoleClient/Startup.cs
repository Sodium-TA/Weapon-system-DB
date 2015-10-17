namespace WeaponSystem.TestConsoleClient
{
    using System.Linq;
    using WeaponSystem.MsSql.Data;

    class Startup
    {
        static void Main(string[] args)
        {
            var db = new WeaponSystemContext();

            db.Weapons.ToList();
            db.SaveChanges();
        }
    }
}
