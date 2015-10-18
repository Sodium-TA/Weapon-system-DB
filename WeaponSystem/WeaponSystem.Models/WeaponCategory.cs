namespace WeaponSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WeaponCategory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public int? MainCategoryId { get; set; }

        public WeaponCategory MainCategory { get; set; }
    }
}
