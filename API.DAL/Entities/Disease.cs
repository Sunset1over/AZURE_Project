namespace API.DAL.Entities;

public class Disease
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }

    public Guid AnimalId { get; set; }
    public Animal? Animal { get; set; }
}
