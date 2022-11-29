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
    public class DiseasesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DiseasesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("all/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetDiseases(Guid id)
        {
            var animal = await _context.Animals
                .FirstOrDefaultAsync(x => x.Id == id);

            if (animal == null)
            {
                return NotFound("No animal found");
            }

            return Ok(new
            {
                    animal.Name,
                    animal.Type,
                    animal.Age,
                    animal.ContactData,
                Diseases = await _context.Diseases.Where(x => x.AnimalId == id).ToListAsync()
            });
        }

        [HttpPost("create")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Disease>> PostDisease(Disease model)
        {
            model.DateTimeStart = DateTime.UtcNow;
            await _context.Diseases.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        [HttpPut("edit/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PutDisease(Guid id, Disease model)
        {
            if (id != model.Id) { return BadRequest("UserId is not same in model and Guid"); }

            var disease = await _context.Diseases.FindAsync(model.Id);

            if (disease == null)
            {
                return NotFound("No disease found");
            }

            disease.Name = model.Name;
            disease.Type = model.Type;
            disease.DateTimeEnd = model.DateTimeEnd;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteDisease(Guid id)
        {
            var disease = await _context.Diseases.FindAsync(id);

            if (disease == null)
            {
                return NotFound("No disease found");
            }

            _context.Diseases.Remove(disease);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
