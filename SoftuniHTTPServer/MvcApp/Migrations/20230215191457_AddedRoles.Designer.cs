// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcApp.Data;

#nullable disable

namespace MvcApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230215191457_AddedRoles")]
    partial class AddedRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MvcApp.Data.Commit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("RepositoryId");

                    b.ToTable("Commits");
                });

            modelBuilder.Entity("MvcApp.Data.Repository", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Repositories");
                });

            modelBuilder.Entity("MvcApp.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MvcApp.Data.Commit", b =>
                {
                    b.HasOne("MvcApp.Data.User", "Creator")
                        .WithMany("Commits")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("MvcApp.Data.Repository", "Repository")
                        .WithMany("Commits")
                        .HasForeignKey("RepositoryId");

                    b.Navigation("Creator");

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("MvcApp.Data.Repository", b =>
                {
                    b.HasOne("MvcApp.Data.User", "Owner")
                        .WithMany("Repositories")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("MvcApp.Data.Repository", b =>
                {
                    b.Navigation("Commits");
                });

            modelBuilder.Entity("MvcApp.Data.User", b =>
                {
                    b.Navigation("Commits");

                    b.Navigation("Repositories");
                });
#pragma warning restore 612, 618
        }
    }
}
