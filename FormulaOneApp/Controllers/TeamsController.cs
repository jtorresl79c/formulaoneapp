using FormulaOneApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace FormulaOneApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private static List<Team> teams = new List<Team>()
        {
            new Team()
            {
                Country = "Germany",
                Id = 1,
                Name = "Mercedes AMG 1",
                TeamPrinciple = "Toto Wolf"
            },
            new Team()
            {
                Country = "Italy",
                Id = 2,
                Name = "Ferrari",
                TeamPrinciple = "Mattia Binotto"
            },
            new Team()
            {
                Country = "Swiss",
                Id = 3,
                Name = "Alpha Romeo",
                TeamPrinciple = "Frederic Vasseur"
            }
        };

        [HttpGet]
        //[Route("GetBestTeam")]
        public IActionResult Get()
        {
            return Ok(teams);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var team = teams.FirstOrDefault(x => x.Id == id);

            if (team == null)
            {
                return BadRequest("Invalid id");
            }

            return Ok(team);
        }

        [HttpPost]
        public IActionResult Post(Team team)
        {
            teams.Add(team);

            // En un Post response se deben de retornar 3 cosas
            // - El response 201
            // - El objeto que se acaba de agregar
            // - La url en donde podemos obtener por ID el objeto que acabamos de agregar
            // CreatedAtAction("Es el action que se llama Get, no se refiere al tipo de http request")
            return CreatedAtAction("Get", team.Id, team);
        }

        [HttpPatch]
        public IActionResult Patch(int id, string country) // Un patch actualiza parcialmente un objeto, un put actualiza todo el objeto 
        {
            var team = teams.FirstOrDefault(x => x.Id == id);

            if (team == null)
                return BadRequest("Invalid id");

            team.Country = country;

            // Un Patch debe de retornar un 204 sin ninguna otra informacion extra
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var team = teams.FirstOrDefault(x => x.Id == id);

            if (team == null)
                return BadRequest("Invalid id");

            teams.Remove(team);

            return NoContent();
        }
    }
}
