using api_beach_parking_csharp.Models.Dto;

namespace api_beach_parking_csharp.Datos
{
    public class CarStore
    {
        public static List<CarDto> cars = new List<CarDto>
        {
            new CarDto{id=1, code="REF-717", name="Wolkswagen Gol Azul", model="Wolkswagen", color="azul", seats_quantity=5, status=1},
            new CarDto{id=2, code="A1A-123", name="Nissan 2023", model="Nissan", color="rojo", seats_quantity=5, status=1}
        };
    }
}
