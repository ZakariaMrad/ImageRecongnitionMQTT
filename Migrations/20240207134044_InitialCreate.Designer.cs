﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ImageRecongnitionMQTT.Migrations
{
    [DbContext(typeof(ImageRecognitionContext))]
    [Migration("20240207134044_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeamModel", b =>
                {
                    b.Property<int>("IdBeam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBeam"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("IdBeam");

                    b.ToTable("Beams");
                });

            modelBuilder.Entity("ItemModel", b =>
                {
                    b.Property<int>("IdItem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdItem"));

                    b.Property<int?>("BeamModelIdBeam")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("IdItem");

                    b.HasIndex("BeamModelIdBeam");

                    b.ToTable("ItemModel");
                });

            modelBuilder.Entity("PositionModel", b =>
                {
                    b.Property<int>("IdPosition")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPosition"));

                    b.Property<int?>("BeamModelIdBeam")
                        .HasColumnType("int");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("IdPosition");

                    b.HasIndex("BeamModelIdBeam");

                    b.ToTable("PositionModel");
                });

            modelBuilder.Entity("ItemModel", b =>
                {
                    b.HasOne("BeamModel", null)
                        .WithMany("Items")
                        .HasForeignKey("BeamModelIdBeam");
                });

            modelBuilder.Entity("PositionModel", b =>
                {
                    b.HasOne("BeamModel", null)
                        .WithMany("Corners")
                        .HasForeignKey("BeamModelIdBeam");
                });

            modelBuilder.Entity("BeamModel", b =>
                {
                    b.Navigation("Corners");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
