namespace Thallo.Models;

public class LogEntry
{
    public int Id { get; set; }
    public string Type { get; set; }
    public double? Value { get; set; }
    public string? Message { get; set; }

    public double? Calories { get; set; }
public double? Sugar { get; set; }
public double? Salt { get; set; }
public double? Protein { get; set; }

    public DateTime Timestamp { get; set; }
}