using System.ComponentModel.DataAnnotations;

namespace MDP3.Tables
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;


        public int ProducerId { get; set; }     // FK
        public Producer? Producer { get; set; } // Навигация
    }
}
