namespace API.DAL.Entities;

public class Animal
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Age { get; set; }
    public string ContactData { get; set; } = string.Empty;

    public List<Disease> Diseases = new List<Disease>();
    public List<Measurement> Measurements = new List<Measurement>();
}
