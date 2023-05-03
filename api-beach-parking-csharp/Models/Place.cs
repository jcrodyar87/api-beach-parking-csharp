using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_beach_parking_csharp.Models
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int client_id { get; set; }
        public int car_id { get; set; }
        public double price { get; set; }
        public DateTime parking_date { get; set; }
        public int status { get; set; }

        public DateTime creation_date { get; set; }
        public DateTime updated_date { get; set; }
    }
}
