using api_beach_parking_csharp.Datos;
using api_beach_parking_csharp.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_beach_parking_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet(Name = "GetCars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CarDto>> GetCars()
        {
            return Ok(CarStore.cars);
        }

        [HttpGet("id:int", Name = "GetCarById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarDto> GetCarById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var car = CarStore.cars.FirstOrDefault(v => v.id == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost(Name = "CreateCar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CarDto> CreateCar([FromBody] CarDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(CarStore.cars.FirstOrDefault(v=>v.code.ToLower() == carDto.code.ToLower()) != null)
            {
                ModelState.AddModelError("CodigoExiste", "Este código de auto ya ha sido usado");
                return BadRequest(ModelState);
            }

            if (carDto == null)
            {
                return BadRequest(carDto);
            }

            if (carDto.id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            carDto.id = CarStore.cars.OrderByDescending(v => v.id).FirstOrDefault().id + 1;
            CarStore.cars.Add(carDto);

            return CreatedAtRoute("GetCarById", new { id = carDto.id, carDto });

        }

        [HttpDelete("{id:int}", Name = "DeleteCar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCar(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var car = CarStore.cars.FirstOrDefault(c => c.id == id);

            if (car == null)
            {
                return NotFound();
            }

            CarStore.cars.Remove(car);

            return NoContent();
        }
    }
}
