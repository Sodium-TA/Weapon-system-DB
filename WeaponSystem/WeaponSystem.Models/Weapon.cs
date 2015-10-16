namespace WeaponSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Weapon
    {
        private ICollection<Target> targets;

        public Weapon()
        {
            this.targets = new HashSet<Target>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public int TypeId { get; set; }

        public virtual Type Type { get; set; }

        public int WeaponCategoryId { get; set; }

        public virtual WeaponCategory WeaponCategory { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public  int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public ushort RelaseYear { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<Target> Targets
        {
            get { return this.targets; }
            set { this.targets = value; }
        } 
    }
}
