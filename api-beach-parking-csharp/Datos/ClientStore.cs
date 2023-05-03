using api_beach_parking_csharp.Models.Dto;

namespace api_beach_parking_csharp.Datos
{
    public class ClientStore
    {
        public static List<ClientDto> clients = new List<ClientDto>
        {
            new ClientDto{id=1, first_name="Juan", last_name="Rodriguez", email="jcry87@gmail.com", phone="999999999"},
            new ClientDto{id=2, first_name="Carlos", last_name="Rodriguez", email="jcry1987@gmail.com", phone="909999999"}
        };
    }
}
