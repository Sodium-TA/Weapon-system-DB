namespace WeaponSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Target
    {
        private ICollection<Weapon> weapons;

        public Target()
        {
            this.weapons = new HashSet<Weapon>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public int TargetCategoryId { get; set; }

        public virtual TargetCategory TargetCategory { get; set; }

        public virtual ICollection<Weapon>  Weapons
        {
            get { return this.weapons; }
            set { this.weapons = value; }
        } 
    }
}
