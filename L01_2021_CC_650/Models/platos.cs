using System.ComponentModel.DataAnnotations;

namespace L01_2021_CC_650.Models
{
    public class Platos
    {
        [Key]
        public int platoId { get; set; }
        public string? nombrePlato { get; set; }
        public decimal precio { get; set; }
    }
}
