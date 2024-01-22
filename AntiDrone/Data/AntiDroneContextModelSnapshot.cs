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

                    b.Property<string>("approval_state")
                        .IsRequired()
                        .HasColumnType("longtext");

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

                    b.Property<long>("authority")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("join_datetime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("latest_access_datetime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("member_contact")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("member_email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("member_id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("member_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("member_pw")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("permission_state")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("Member");
                });
#pragma warning restore 612, 618
        }
    }
}
