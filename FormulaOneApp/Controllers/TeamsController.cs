using FormulaOneApp.Data;
using FormulaOneApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FormulaOneApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        //private static List<Team> teams = new List<Team>()
        //{
        //    new Team()
        //    {
        //        Country = "Germany",
        //        Id = 1,
        //        Name = "Mercedes AMG 1",
        //        TeamPrinciple = "Toto Wolf"
        //    },
        //    new Team()
        //    {
        //        Country = "Italy",
        //        Id = 2,
        //        Name = "Ferrari",
        //        TeamPrinciple = "Mattia Binotto"
        //    },
        //    new Team()
        //    {
        //        Country = "Swiss",
        //        Id = 3,
        //        Name = "Alpha Romeo",
        //        TeamPrinciple = "Frederic Vasseur"
        //    }
        //};


        private static AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Route("GetBestTeam")]
        public async Task<IActionResult> Get()
        {
            var teams = await _context.Teams.ToListAsync();

            return Ok(teams);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return BadRequest("Invalid id");
            }

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();

            // En un Post response se deben de retornar 3 cosas
            // - El response 201
            // - El objeto que se acaba de agregar
            // - La url en donde podemos obtener por ID el objeto que acabamos de agregar
            // CreatedAtAction("Es el action que se llama Get, no se refiere al tipo de http request")
            return CreatedAtAction("Get", team.Id, team);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(int id, string country) // Un patch actualiza parcialmente un objeto, un put actualiza todo el objeto 
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
                return BadRequest("Invalid id");

            team.Country = country;
            await _context.SaveChangesAsync();

            // Un Patch debe de retornar un 204 sin ninguna otra informacion extra
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
                return BadRequest("Invalid id");

            _context.Teams.Remove(team);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
