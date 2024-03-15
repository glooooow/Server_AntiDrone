using System.ComponentModel;
using AntiDrone.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DateOnlyConverter = AntiDrone.Utils.DateOnlyConverter;
using AntiDrone.Models.Systems.Member;
using TimeOnlyConverter = AntiDrone.Utils.TimeOnlyConverter;

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
            
            base.ConfigureConventions(builder);
            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>();
        }
        
        
        // 관제 시스템 데이터 모델 -> DB화
        public DbSet<AntiDrone.Models.Systems.DroneControl.Whitelist> Whitelist { get; set; } = default!;
        public DbSet<AntiDrone.Models.Systems.Member.Member> Member { get; set; } = default!;
        public DbSet<AntiDrone.Models.Systems.Member.MemberLog> MemberLog { get; set; } = default!;

        // 탐지 데이터 모델 -> DB화
        public DbSet<AntiDrone.Models.Detections.ScannerDetections> ScannerDetections { get; set; } = default!;
        public DbSet<AntiDrone.Models.Detections.HistoryHeader> HistoryHeader { get; set; } = default!;
    }
}