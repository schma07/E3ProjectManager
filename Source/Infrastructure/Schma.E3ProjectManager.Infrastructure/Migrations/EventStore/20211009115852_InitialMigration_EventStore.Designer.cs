﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Schma.E3ProjectManager.Infrastructure.DbContexts;

#nullable disable

namespace Schma.E3ProjectManager.Infrastructure.Migrations.EventStore
{
    [DbContext(typeof(EventStoreDbContext))]
    [Migration("20211009115852_InitialMigration_EventStore")]
    partial class InitialMigration_EventStore
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("EventStore")
                .HasAnnotation("ProductVersion", "6.0.0-rc.1.21452.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Schma.E3ProjectManager.Infrastructure.Models.AggregateSnapshotEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AggregateId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AggregateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastAggregateVersion")
                        .HasColumnType("int");

                    b.Property<Guid>("LastEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Snapshots", (string)null);
                });

            modelBuilder.Entity("Schma.E3ProjectManager.Infrastructure.Models.BranchPointEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("Name", "EventId")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("BranchPoints", (string)null);
                });

            modelBuilder.Entity("Schma.E3ProjectManager.Infrastructure.Models.EventEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AggregateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AggregateName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssemblyTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AggregateId", "Version", "AggregateName")
                        .IsUnique()
                        .HasFilter("[AggregateId] IS NOT NULL AND [AggregateName] IS NOT NULL");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("AggregateId", "Version", "AggregateName"), false);

                    b.ToTable("Events", (string)null);
                });

            modelBuilder.Entity("Schma.E3ProjectManager.Infrastructure.Models.RetroactiveEventEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssemblyTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BranchPointId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchPointId");

                    b.ToTable("RetroactiveEvents", (string)null);
                });

            modelBuilder.Entity("Schma.E3ProjectManager.Infrastructure.Models.BranchPointEntity", b =>
                {
                    b.HasOne("Schma.E3ProjectManager.Infrastructure.Models.EventEntity", "Event")
                        .WithMany("BranchPoints")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Schma.E3ProjectManager.Infrastructure.Models.RetroactiveEventEntity", b =>
                {
                    b.HasOne("Schma.E3ProjectManager.Infrastructure.Models.BranchPointEntity", "BranchPoint")
                        .WithMany("RetroactiveEvents")
                        .HasForeignKey("BranchPointId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BranchPoint");
                });

            modelBuilder.Entity("Schma.E3ProjectManager.Infrastructure.Models.BranchPointEntity", b =>
                {
                    b.Navigation("RetroactiveEvents");
                });

            modelBuilder.Entity("Schma.E3ProjectManager.Infrastructure.Models.EventEntity", b =>
                {
                    b.Navigation("BranchPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
