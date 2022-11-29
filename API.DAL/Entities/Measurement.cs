namespace API.DAL.Entities;

public class Measurement
{
    public Guid Id { get; set; }
    public double Weight { get; set; }
    public int BloodPressure { get; set; }
    public double Temperature { get; set; }
    public int Pulse { get; set; }
    public int BreathingRate { get; set; }
    public Guid AnimalId { get; set; }
    public DateTime DateTime { get; set; }
    public Animal? Animal { get; set; }
}
