using System.ComponentModel.DataAnnotations;

namespace L01_2021_CC_650.Models
{
    public class Motoristas
    {
        [Key]
        public int motoristaId { get; set; }
        public string? nombreMotorista { get; set; }
    }
}
