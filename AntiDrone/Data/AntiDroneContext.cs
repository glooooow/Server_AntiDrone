using System.ComponentModel;
using AntiDrone.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DateOnlyConverter = AntiDrone.Utils.DateOnlyConverter;
using AntiDrone.Models.Systems.Member;

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
        public DbSet<AntiDrone.Models.Systems.Member.Member> Member { get; set; } = default!;
        public DbSet<AntiDrone.Models.Systems.Member.MemberLog> MemberLog { get; set; } = default!;
    }
}