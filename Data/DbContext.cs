using Microsoft.EntityFrameworkCore;
using Thallo.Models;   // wherever LogEntry lives

namespace Thallo.Data;

public class ThalloContext : DbContext
{
    public ThalloContext(DbContextOptions<ThalloContext> options)
        : base(options)
    {
    }

    public DbSet<LogEntry> Logs => Set<LogEntry>();
    public DbSet<Shortcut> Shortcuts => Set<Shortcut>();
    public DbSet<DailyHealth> DailyHealth => Set<DailyHealth>();
}