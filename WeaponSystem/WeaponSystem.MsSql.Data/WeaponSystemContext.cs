namespace WeaponSystem.MsSql.Data
{
    using System.Data.Entity;

    using WeaponSystem.Models;

    public class WeaponSystemContext : DbContext
    {
        public WeaponSystemContext()
            : base("name=WeaponSystemContext")
        {
        }

        public virtual IDbSet<Weapon> Weapons { get; set; }

        public virtual IDbSet<Target> Targets { get; set; }

        public virtual IDbSet<TargetCategory> TargetCategories { get; set; }

        public virtual IDbSet<WeaponCategory> WeaponCategoies { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }

        public virtual IDbSet<Caliber> Calibers { get; set; } 

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}