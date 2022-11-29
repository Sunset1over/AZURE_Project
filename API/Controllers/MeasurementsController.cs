using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DAL.EF;
using API.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public MeasurementsController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "User")]
        [HttpGet("all/{id}")]
        public async Task<IActionResult> GetMeasurements(Guid id)
        {
            var animal = await _context.Animals
                .SingleOrDefaultAsync(x => x.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(new {
                animal.Name,
                animal.Type,
                animal.Age,
                animal.ContactData,
                measurements = await _context.Measurements.Where(x => x.AnimalId == id).OrderByDescending(x => x.DateTime).Select(x => new {
                    x.Id,
                    x.Weight,
                    x.BloodPressure,
                    x.Temperature,
                    x.Pulse,
                    x.BreathingRate,
                    x.DateTime
                }).ToListAsync()
            });
        }

        [Authorize(Roles = "User")]
        [HttpPost("createRandom/{id}")]
        public async Task<ActionResult<Measurement>> PostMeasurement(Guid id)
        {
            var animal = await _context.Animals
                .SingleOrDefaultAsync(x => x.Id == id);

            if (animal == null)
            {
                return BadRequest();
            }

            Measurement measurement = new Measurement {
                Weight = new Random().NextDouble(1.5, 70.0),
                BloodPressure = new Random().Next(70, 180),
                Temperature = new Random().NextDouble(36.0, 40.0),
                Pulse = new Random().Next(70, 140),
                BreathingRate = new Random().Next(14, 40),
                DateTime = DateTime.Now,
                AnimalId = animal.Id,
            };

            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                measurement.Id,
                measurement.Weight,
                measurement.BloodPressure,
                measurement.Temperature,
                measurement.Pulse,
                measurement.BreathingRate,
                measurement.DateTime
            });
        }
    }

    public static class RandomExtensions
    {
        public static double NextDouble(
            this Random random,
            double minValue,
            double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
