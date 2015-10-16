namespace WeaponSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class WeaponCategory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public int? MainCategoryId { get; set; }

        public WeaponCategory MainCategory { get; set; }
    }
}
