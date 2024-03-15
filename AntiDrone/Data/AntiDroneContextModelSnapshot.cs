﻿// <auto-generated />
using System;
using AntiDrone.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AntiDrone.Data
{
    [DbContext(typeof(AntiDroneContext))]
    partial class AntiDroneContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AntiDrone.Models.Detections.HistoryHeader", b =>
                {
                    b.Property<long>("meta_master_key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("det_date")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeOnly>("det_end_time")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("det_start_time")
                        .HasColumnType("time");

                    b.Property<string>("meta_data_code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("meta_data_id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("meta_data_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("meta_data_type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("meta_memo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("meta_mfr")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("meta_near_lat")
                        .HasColumnType("double");

                    b.Property<double>("meta_near_lon")
                        .HasColumnType("double");

                    b.Property<double>("meta_near_operator_lat")
                        .HasColumnType("double");

                    b.Property<double>("meta_near_operator_lon")
                        .HasColumnType("double");

                    b.Property<string>("meta_writer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("meta_master_key");

                    b.ToTable("HistoryHeader");
                });

            modelBuilder.Entity("AntiDrone.Models.Detections.ScannerDetections", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<double>("altitude")
                        .HasColumnType("double");

                    b.Property<double>("angle")
                        .HasColumnType("double");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("course")
                        .HasColumnType("double");

                    b.Property<DateTime>("det_date")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeOnly>("det_time")
                        .HasColumnType("time");

                    b.Property<double>("elevation")
                        .HasColumnType("double");

                    b.Property<double>("frequency")
                        .HasColumnType("double");

                    b.Property<double>("latitude")
                        .HasColumnType("double");

                    b.Property<double>("longitude")
                        .HasColumnType("double");

                    b.Property<string>("mac_address1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("mac_address2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("mfr")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("operator_lat")
                        .HasColumnType("double");

                    b.Property<double>("operator_lon")
                        .HasColumnType("double");

                    b.Property<string>("port")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("protocol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("range")
                        .HasColumnType("double");

                    b.Property<string>("sc_det_id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("velocity")
                        .HasColumnType("double");

                    b.HasKey("id");

                    b.ToTable("ScannerDetections");
                });

            modelBuilder.Entity("AntiDrone.Models.Systems.DroneControl.Whitelist", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("affiliation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("approval_end_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("approval_start_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("approval_state")
                        .HasColumnType("int");

                    b.Property<string>("contact")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("drone_id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("drone_model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("drone_type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("memo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("now_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("operator_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Whitelist");
                });

            modelBuilder.Entity("AntiDrone.Models.Systems.Member.Member", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("authority")
                        .HasColumnType("int");

                    b.Property<DateTime>("join_datetime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("latest_access_datetime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("member_id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("member_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("member_pw")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("permission_state")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("AntiDrone.Models.Systems.Member.MemberLog", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("memlog_datetime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("memlog_from")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("memlog_level")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("memlog_to")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("memlog_type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("MemberLog");
                });
#pragma warning restore 612, 618
        }
    }
}
