using API.BLL.DTO.Request;
using API.BLL.DTO.Response;
using API.DAL.EF;
using API.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AnimalsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        [HttpPost("create")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Animal>> PostAnimal(Animal model)
        {
            await _context.Animals.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        [HttpPut("edit/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PutAnimal(Guid id, Animal model)
        {
            if (id != model.Id) { return BadRequest("UserId is not same in model and Guid"); }

            var animal = await _context.Animals.FindAsync(model.Id);

            if (animal == null)
            {
                return NotFound("No animal found");
            }

            animal.Name = model.Name;
            animal.Type = model.Type;
            animal.Age = model.Age;
            animal.ContactData = model.ContactData;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAnimal(Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound("No animal found");
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
