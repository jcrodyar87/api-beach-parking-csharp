namespace api_beach_parking_csharp.Models
{
    public class Place
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public int car_id { get; set; }
        public int status { get; set; }

        public DateTime creation_date { get; set; }
        public DateTime updated_date { get; set; }
    }
}
