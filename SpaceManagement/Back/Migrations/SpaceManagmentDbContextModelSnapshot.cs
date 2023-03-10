// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpaceManagment.Data;

#nullable disable

namespace SpaceManagment.Migrations
{
    [DbContext(typeof(SpaceManagmentDbContext))]
    partial class SpaceManagmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("SpaceManagment.Model.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RefreshTokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RefreshTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("SpaceManagment.Model.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long?>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<int>("EndDate")
                        .HasColumnType("int");

                    b.Property<int>("EndTime")
                        .HasColumnType("int");

                    b.Property<int>("InitiatorType")
                        .HasColumnType("int");

                    b.Property<int>("LastSupervisorId")
                        .HasColumnType("int");

                    b.Property<int?>("SpaceActiveIntervalId")
                        .HasColumnType("int");

                    b.Property<int?>("SpaceSupervisorId")
                        .HasColumnType("int");

                    b.Property<int>("StartDate")
                        .HasColumnType("int");

                    b.Property<int>("StartTime")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long?>("SupervisorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SpaceActiveIntervalId");

                    b.HasIndex("SpaceSupervisorId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("SpaceManagment.Model.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("SpaceManagment.Model.Space", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<long>("HostId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("Spaces");
                });

            modelBuilder.Entity("SpaceManagment.Model.SpaceActiveInterval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EndDate")
                        .HasColumnType("int");

                    b.Property<int>("EndTime")
                        .HasColumnType("int");

                    b.Property<int?>("SpaceId")
                        .HasColumnType("int");

                    b.Property<int>("StartDate")
                        .HasColumnType("int");

                    b.Property<int>("StartTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.ToTable("SpaceActiveIntervals");
                });

            modelBuilder.Entity("SpaceManagment.Model.SpaceSupervisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("SpaceId")
                        .HasColumnType("int");

                    b.Property<long?>("SupervisorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("SpaceSupervisors");
                });

            modelBuilder.Entity("SpaceManagment.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SpaceManagment.Model.Client", b =>
                {
                    b.HasBaseType("SpaceManagment.Model.User");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("SpaceManagment.Model.Host", b =>
                {
                    b.HasBaseType("SpaceManagment.Model.User");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Host", (string)null);
                });

            modelBuilder.Entity("SpaceManagment.Model.Supervisor", b =>
                {
                    b.HasBaseType("SpaceManagment.Model.User");

                    b.Property<long?>("HostId")
                        .HasColumnType("bigint");

                    b.HasIndex("HostId");

                    b.ToTable("Supervisor", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("SpaceManagment.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("SpaceManagment.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("SpaceManagment.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("SpaceManagment.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpaceManagment.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("SpaceManagment.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpaceManagment.Model.RefreshToken", b =>
                {
                    b.HasOne("SpaceManagment.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpaceManagment.Model.Reservation", b =>
                {
                    b.HasOne("SpaceManagment.Model.Client", null)
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId");

                    b.HasOne("SpaceManagment.Model.SpaceActiveInterval", "SpaceActiveInterval")
                        .WithMany("Reservations")
                        .HasForeignKey("SpaceActiveIntervalId");

                    b.HasOne("SpaceManagment.Model.SpaceSupervisor", "SpaceSupervisor")
                        .WithMany()
                        .HasForeignKey("SpaceSupervisorId");

                    b.HasOne("SpaceManagment.Model.Supervisor", "Supervisor")
                        .WithMany("Reservations")
                        .HasForeignKey("SupervisorId");

                    b.Navigation("SpaceActiveInterval");

                    b.Navigation("SpaceSupervisor");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("SpaceManagment.Model.Space", b =>
                {
                    b.HasOne("SpaceManagment.Model.Host", "Host")
                        .WithMany("Spaces")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Host");
                });

            modelBuilder.Entity("SpaceManagment.Model.SpaceActiveInterval", b =>
                {
                    b.HasOne("SpaceManagment.Model.Space", "Space")
                        .WithMany("SpaceActiveIntervals")
                        .HasForeignKey("SpaceId");

                    b.Navigation("Space");
                });

            modelBuilder.Entity("SpaceManagment.Model.SpaceSupervisor", b =>
                {
                    b.HasOne("SpaceManagment.Model.Space", "Space")
                        .WithMany("SpaceSupervisors")
                        .HasForeignKey("SpaceId");

                    b.HasOne("SpaceManagment.Model.Supervisor", "Supervisor")
                        .WithMany("SpaceSupervisors")
                        .HasForeignKey("SupervisorId");

                    b.Navigation("Space");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("SpaceManagment.Model.Client", b =>
                {
                    b.HasOne("SpaceManagment.Model.User", null)
                        .WithOne()
                        .HasForeignKey("SpaceManagment.Model.Client", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpaceManagment.Model.Host", b =>
                {
                    b.HasOne("SpaceManagment.Model.User", null)
                        .WithOne()
                        .HasForeignKey("SpaceManagment.Model.Host", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpaceManagment.Model.Supervisor", b =>
                {
                    b.HasOne("SpaceManagment.Model.Host", "Host")
                        .WithMany("Supervisors")
                        .HasForeignKey("HostId");

                    b.HasOne("SpaceManagment.Model.User", null)
                        .WithOne()
                        .HasForeignKey("SpaceManagment.Model.Supervisor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Host");
                });

            modelBuilder.Entity("SpaceManagment.Model.Space", b =>
                {
                    b.Navigation("SpaceActiveIntervals");

                    b.Navigation("SpaceSupervisors");
                });

            modelBuilder.Entity("SpaceManagment.Model.SpaceActiveInterval", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("SpaceManagment.Model.Client", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("SpaceManagment.Model.Host", b =>
                {
                    b.Navigation("Spaces");

                    b.Navigation("Supervisors");
                });

            modelBuilder.Entity("SpaceManagment.Model.Supervisor", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("SpaceSupervisors");
                });
#pragma warning restore 612, 618
        }
    }
}
