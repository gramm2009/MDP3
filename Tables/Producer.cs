using System.ComponentModel.DataAnnotations;

namespace MDP3.Tables
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
