namespace Thallo.Models;

public class Shortcut
{
    public int Id { get; set; }
    public string Label { get; set; } = "";
    public string Type { get; set; } = "";
    public string? Icon { get; set; }
    public double? Value { get; set; }       // null = value-less event (cigarette, bathroom)
    public string? Unit { get; set; }         // "mg", "ml", "min", or null
    public string Color { get; set; } = "#84cf6e";
    public string Aggregation { get; set; } = "count";  // "count" or "sum"
    public bool ShowTally { get; set; } = true;
    public int SortOrder { get; set; }        // so you can control button order
}