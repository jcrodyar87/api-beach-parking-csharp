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
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ApplicationDbContext _db;

        public ClientController(ILogger<ClientController> logger, ApplicationDbContext db) 
        { 
            _logger = logger;
            _db = db;
        }

        [HttpGet(Name="GetClients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ClientDto>> GetClients()
        {
            _logger.LogInformation("All clients have been loaded");
            return Ok(_db.clients.ToList());
        }

        [HttpGet("id:int", Name = "GetClientById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ClientDto> GetClientById(int id)
        {
            if(id == 0) 
            {
                _logger.LogError("Error when you try load this client " + id);
                return BadRequest();
            }
            //var client = ClientStore.clients.FirstOrDefault(v => v.id == id);
            var client = _db.clients.FirstOrDefault(v => v.id == id);

            if(client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost(Name = "CreateClient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClientDto> CreateClient([FromBody] ClientDto clientDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(clientDto == null) 
            {
                return BadRequest(clientDto);
            }

            if(clientDto.id > 0) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            
            }
            Client client = new()
            {
                id = clientDto.id,
                first_name = clientDto.first_name,
                last_name = clientDto.last_name,
                email = clientDto.email,
                phone = clientDto.phone,
                status = clientDto.status,
            };

            _db.clients.Add(client);
            _db.SaveChanges();

            return CreatedAtRoute("GetClientById", new { id=clientDto.id, clientDto});

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateClient(int id, [FromBody] ClientDto clientDto)
        {
            if(clientDto == null || id!= clientDto.id)
            {
                return BadRequest();
            }

            Client client = new()
            {
                id = clientDto.id,
                first_name = clientDto.first_name,
                last_name = clientDto.last_name,
                email = clientDto.email,
                phone = clientDto.phone,
                status = clientDto.status,
            };

            _db.clients.Update(client);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialClient(int id, JsonPatchDocument<ClientDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var client = _db.clients.FirstOrDefault(v => v.id == id);

            ClientDto clientDto = new()
            {
                id = client.id,
                first_name = client.first_name,
                last_name = client.last_name,
                email = client.email,
                phone = client.phone,
                status = client.status,
            };

            if (client == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(clientDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client model = new()
            {
                id = clientDto.id,
                first_name = clientDto.first_name,
                last_name = clientDto.last_name,
                email = clientDto.email,
                phone = clientDto.phone,
                status = clientDto.status,
            };
            _db.clients.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}", Name="DeleteClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteClient(int id)
        {
            if(id==0) 
            { 
                return BadRequest(); 
            }

            var client = _db.clients.FirstOrDefault(c => c.id == id);

            if(client == null)
            {
                return NotFound();
            }

            _db.clients.Remove(client);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
