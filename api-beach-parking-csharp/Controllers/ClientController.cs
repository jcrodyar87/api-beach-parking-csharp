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

        public ClientController(ILogger<ClientController> logger) 
        { 
            _logger = logger;
        }

        [HttpGet(Name="GetClients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ClientDto>> GetClients()
        {
            _logger.LogInformation("All client have been loaded");
            return Ok(ClientStore.clients);
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
            var client = ClientStore.clients.FirstOrDefault(v => v.id == id);

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

            clientDto.id = ClientStore.clients.OrderByDescending(v => v.id).FirstOrDefault().id + 1;
            ClientStore.clients.Add(clientDto);

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
            var client = ClientStore.clients.FirstOrDefault(c => c.id == id);
            client.first_name = clientDto.first_name;
            client.last_name = clientDto.last_name;
            client.email = clientDto.email;
            client.phone = clientDto.phone;
            client.status = clientDto.status;

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

            var client = ClientStore.clients.FirstOrDefault(c => c.id == id);
            patchDto.ApplyTo(client, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);   
            }

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

            var client = ClientStore.clients.FirstOrDefault(c => c.id == id);

            if(client == null)
            {
                return NotFound();
            }

            ClientStore.clients.Remove(client);

            return NoContent();
        }
    }
}
