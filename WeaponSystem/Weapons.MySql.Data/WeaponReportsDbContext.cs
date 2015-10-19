namespace Weapons.MySqlDb.Data
{
    using System;
    using System.Data.Entity;

    using Weapons.MySqlDb.Models;

    public class WeaponReportsDbContext : DbContext
    {
        public WeaponReportsDbContext()
            : this("weapon_reports_db")
        {
        }

        public WeaponReportsDbContext(string connName)
            : base(connName)
        {
        }

        public virtual DbSet<WeaponReports> WeaponReports { get; set; }
    }
}
