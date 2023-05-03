using System.ComponentModel.DataAnnotations;

namespace api_beach_parking_csharp.Models.Dto
{
    public class ClientDto
    {
        public int id { get; set; }
        [Required]
        [MaxLength(255)]
        public string last_name { get; set; }
        [Required]
        [MaxLength(255)]
        public string first_name { get; set; }
        [Required]
        [MaxLength(100)]
        public string email { get; set; }
        [Required]
        [MaxLength(20)]
        public string phone { get; set; }
        public int status { get; set; }
    }
}
