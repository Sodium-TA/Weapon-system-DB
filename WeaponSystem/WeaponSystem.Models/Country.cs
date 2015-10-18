namespace WeaponSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Country
    {
        private ICollection<Manufacturer> manufacturers;

        public Country()
        {
            this.manufacturers = new HashSet<Manufacturer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string Code { get; set; }

        public virtual ICollection<Manufacturer> Manufacturers
        {
            get { return this.manufacturers; }
            set { this.manufacturers = value; }
        }
    }
}
