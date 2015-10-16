namespace WeaponSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Country
    {
        private ICollection<Weapon> weapons;

        public Country()
        {
            this.weapons = new HashSet<Weapon>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(60)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(2)]
        public  string Code { get; set; }

        public virtual ICollection<Weapon> Weapons
        {
            get { return this.weapons; }
            set { this.weapons = value; }
        }
    }
}
