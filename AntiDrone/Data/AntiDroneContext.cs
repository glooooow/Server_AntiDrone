using System.ComponentModel;
using AntiDrone.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DateOnlyConverter = AntiDrone.Utils.DateOnlyConverter;

namespace AntiDrone.Data
{
    public class AntiDroneContext : DbContext
    {
        public AntiDroneContext(DbContextOptions<AntiDroneContext> options)
            : base(options)
        {
        }
        
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>();
        }
        
        public DbSet<AntiDrone.Models.Systems.DroneControl.Whitelist> Whitelist { get; set; } = default!;
    }
}