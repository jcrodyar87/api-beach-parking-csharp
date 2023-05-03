using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_beach_parking_csharp.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public int seats_quantity { get; set; }
        public int status { get; set; }

        public DateTime creation_date { get; set; }
        public DateTime updated_date { get; set; }
    }
}
