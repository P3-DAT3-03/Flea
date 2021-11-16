﻿// <auto-generated />
using System;
using Flea.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Flea.Migrations
{
    [DbContext(typeof(BingoContext))]
    [Migration("20211115102432_phonenumber_to_string")]
    partial class phonenumber_to_string
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Flea.Models.Cluster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CustomerCount")
                        .HasColumnType("integer");

                    b.Property<int?>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Cluster");
                });

            modelBuilder.Entity("Flea.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Flea.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("PreviousEventId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PreviousEventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Flea.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("EventId")
                        .HasColumnType("integer");

                    b.Property<bool>("Paid")
                        .HasColumnType("boolean");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<int?>("ReservationOwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("TableCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ReservationOwnerId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Flea.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ClusterId")
                        .HasColumnType("integer");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClusterId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Flea.Models.Cluster", b =>
                {
                    b.HasOne("Flea.Models.Event", null)
                        .WithMany("Clusters")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("Flea.Models.Event", b =>
                {
                    b.HasOne("Flea.Models.Event", "PreviousEvent")
                        .WithMany()
                        .HasForeignKey("PreviousEventId");

                    b.Navigation("PreviousEvent");
                });

            modelBuilder.Entity("Flea.Models.Reservation", b =>
                {
                    b.HasOne("Flea.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("Flea.Models.Customer", "ReservationOwner")
                        .WithMany()
                        .HasForeignKey("ReservationOwnerId");

                    b.Navigation("Event");

                    b.Navigation("ReservationOwner");
                });

            modelBuilder.Entity("Flea.Models.Table", b =>
                {
                    b.HasOne("Flea.Models.Cluster", "Cluster")
                        .WithMany("Tables")
                        .HasForeignKey("ClusterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flea.Models.Reservation", "Reservation")
                        .WithMany("Tables")
                        .HasForeignKey("ReservationId");

                    b.Navigation("Cluster");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Flea.Models.Cluster", b =>
                {
                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Flea.Models.Event", b =>
                {
                    b.Navigation("Clusters");
                });

            modelBuilder.Entity("Flea.Models.Reservation", b =>
                {
                    b.Navigation("Tables");
                });
#pragma warning restore 612, 618
        }
    }
}
