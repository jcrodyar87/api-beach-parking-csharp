using System.ComponentModel.DataAnnotations;

namespace api_beach_parking_csharp.Models.Dto
{
    public class CarDto
    {
        public int id { get; set; }
        [Required]
        [MaxLength(10)]
        public string code { get; set; }
        [Required]
        [MaxLength(255)]
        public string name { get; set; }
        [Required]
        [MaxLength(50)]
        public string model { get; set; }
        [Required]
        [MaxLength(50)]
        public string color { get; set; }
        [Required]
        public int seats_quantity { get; set; }
        [Required]
        public int status { get; set; }
    }
}
