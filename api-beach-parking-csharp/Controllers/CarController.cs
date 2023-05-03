using api_beach_parking_csharp.Datos;
using api_beach_parking_csharp.Models;
using api_beach_parking_csharp.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace api_beach_parking_csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ApplicationDbContext _db;

        public CarController(ILogger<ClientController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet(Name = "GetCars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CarDto>> GetCars()
        {
            _logger.LogInformation("All cars have been loaded");
            return Ok(_db.cars.ToList());
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
            var car = _db.cars.FirstOrDefault(v => v.id == id);

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

            if(_db.cars.FirstOrDefault(v=>v.code.ToLower() == carDto.code.ToLower()) != null)
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

            Car car = new()
            {
                id = carDto.id,
                code = carDto.code,
                name = carDto.name,
                model = carDto.model,
                color = carDto.color,
                seats_quantity = carDto.seats_quantity,
                status = carDto.status,
            };

            _db.cars.Add(car);
            _db.SaveChanges();

            return CreatedAtRoute("GetCarById", new { id = carDto.id, carDto });

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCar(int id, [FromBody] CarDto carDto)
        {
            if (carDto == null || id != carDto.id)
            {
                return BadRequest();
            }

            Car car = new()
            {
                id = carDto.id,
                code = carDto.code,
                name = carDto.name,
                model = carDto.model,
                color = carDto.color,
                seats_quantity = carDto.seats_quantity,
                status = carDto.status,
            };


            _db.cars.Update(car);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialCar(int id, JsonPatchDocument<CarDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var car = _db.cars.FirstOrDefault(c => c.id == id);

            CarDto carDto = new()
            {
                id = car.id,
                code = car.code,
                name = car.name,
                model = car.model,
                color = car.color,
                seats_quantity = car.seats_quantity,
                status = car.status,
            };

            if (car == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(carDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Car model = new()
            {
                id = carDto.id,
                code = carDto.code,
                name = carDto.name,
                model = carDto.model,
                color = carDto.color,
                seats_quantity = carDto.seats_quantity,
                status = carDto.status,
            };

            _db.cars.Update(model);
            _db.SaveChanges();

            return NoContent();
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

            var car = _db.cars.FirstOrDefault(c => c.id == id);

            if (car == null)
            {
                return NotFound();
            }

            _db.cars.Remove(car);
            _db.SaveChanges();


            return NoContent();
        }
    }
}
