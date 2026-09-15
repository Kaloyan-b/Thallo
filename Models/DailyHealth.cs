namespace Thallo.Models;

public class DailyHealth
{
    public int Id { get; set; }
    public DateTime Date { get; set; }      // the day (UTC midnight), unique key for upserts

    // activity
    public int? Steps { get; set; }
    public int? Calories { get; set; }

    // heart rate
    public int? RestingHr { get; set; }
    public int? AvgHr { get; set; }
    public int? MinHr { get; set; }
    public int? MaxHr { get; set; }

    // stress / spo2 (nullable — often "no reading")
    public int? AvgStress { get; set; }
    public int? AvgSpo2 { get; set; }

    // sleep (from the sleep table, durations in minutes)
    public int? SleepTotal { get; set; }
    public int? SleepDeep { get; set; }
    public int? SleepLight { get; set; }
    public int? SleepRem { get; set; }
    public int? SleepAwake { get; set; }
    public DateTime? SleepStart { get; set; }
    public DateTime? SleepEnd { get; set; }
}