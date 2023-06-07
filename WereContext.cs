using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;
using WereWolfMud.Entities;
using WereWolfUltraCool.Entities;

namespace WereWolfUltraCool
{
    public class WereContext : DbContext
    { 
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Leader> Leaders { get; set; }
        public DbSet<TrialVote> TrialVotes { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<LogAccessPermission> LogAccessPermissions { get; set; }
        //There is constructor injection. This must exist to be configurable in Program.cs
        public WereContext(DbContextOptions<WereContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
