﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.DataAccess;

#nullable disable

namespace Server.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Server.Common.Models.AccessToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Revoked")
                        .HasColumnType("bit");

                    b.Property<long>("RtId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RtId");

                    b.ToTable("AccessTokens");
                });

            modelBuilder.Entity("Server.Common.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Server.Common.Models.AccountGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GroupId");

                    b.ToTable("AccountGroups");
                });

            modelBuilder.Entity("Server.Common.Models.AccountModule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ModuleId");

                    b.ToTable("AccountModules");
                });

            modelBuilder.Entity("Server.Common.Models.AccountPermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("PermissionId");

                    b.ToTable("AccountPermissions");
                });

            modelBuilder.Entity("Server.Common.Models.AccountRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("RoleId");

                    b.ToTable("AccountRoles");
                });

            modelBuilder.Entity("Server.Common.Models.Apartment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Available")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BuildingId")
                        .HasColumnType("bigint");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("Server.Common.Models.ApartmentDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<int>("NumBathroom")
                        .HasColumnType("int");

                    b.Property<int>("NumBedroom")
                        .HasColumnType("int");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId")
                        .IsUnique();

                    b.ToTable("ApartmentDetails");
                });

            modelBuilder.Entity("Server.Common.Models.Building", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("BuildingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuildingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint");

                    b.Property<int>("NumberFloor")
                        .HasColumnType("int");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.Property<long>("UrbanId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("UrbanId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("Server.Common.Models.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("BuildingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuildingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Server.Common.Models.DocumentDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("DocumentDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DocumentId")
                        .HasColumnType("bigint");

                    b.Property<string>("DocumentLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("DocumentDetails");
                });

            modelBuilder.Entity("Server.Common.Models.FormResidentRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ModuleId");

                    b.ToTable("FormResidentRequests");
                });

            modelBuilder.Entity("Server.Common.Models.FormResidentRequestDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FormResidentRequestId")
                        .HasColumnType("bigint");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestMediaLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Response")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormResidentRequestId");

                    b.ToTable("FormResidentRequestDetails");
                });

            modelBuilder.Entity("Server.Common.Models.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Server.Common.Models.GroupPermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PermissionId");

                    b.ToTable("GroupPermissions");
                });

            modelBuilder.Entity("Server.Common.Models.Module", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ModuleDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Modules");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ModuleName = "PropertyDossier"
                        },
                        new
                        {
                            Id = 2L,
                            ModuleName = "Form"
                        });
                });

            modelBuilder.Entity("Server.Common.Models.ModulePermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("PermissionId");

                    b.ToTable("ModulePermissions");
                });

            modelBuilder.Entity("Server.Common.Models.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            PermissionName = "CreateAccount"
                        },
                        new
                        {
                            Id = 2L,
                            PermissionName = "ReadAccount"
                        },
                        new
                        {
                            Id = 3L,
                            PermissionName = "UpdateAccount"
                        },
                        new
                        {
                            Id = 4L,
                            PermissionName = "DeleteAccount"
                        },
                        new
                        {
                            Id = 5L,
                            PermissionName = "CreatePropertyDossier"
                        },
                        new
                        {
                            Id = 6L,
                            PermissionName = "ReadPropertyDossier"
                        },
                        new
                        {
                            Id = 7L,
                            PermissionName = "UpdatePropertyDossier"
                        },
                        new
                        {
                            Id = 8L,
                            PermissionName = "DeletePropertyDossier"
                        },
                        new
                        {
                            Id = 9L,
                            PermissionName = "CreateForm"
                        },
                        new
                        {
                            Id = 10L,
                            PermissionName = "ReadForm"
                        },
                        new
                        {
                            Id = 11L,
                            PermissionName = "UpdateForm"
                        },
                        new
                        {
                            Id = 12L,
                            PermissionName = "DeleteForm"
                        },
                        new
                        {
                            Id = 13L,
                            PermissionName = "ReadAllNewResidentRequest"
                        },
                        new
                        {
                            Id = 14L,
                            PermissionName = "UpdateNewResidentRequest"
                        });
                });

            modelBuilder.Entity("Server.Common.Models.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Revoked")
                        .HasColumnType("bit");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Server.Common.Models.Resident", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("ApartmentId")
                        .HasColumnType("bigint");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("ApartmentId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Residents");
                });

            modelBuilder.Entity("Server.Common.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("Server.Common.Models.RolePermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            Id = 3L,
                            PermissionId = 13L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            PermissionId = 14L,
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 5L,
                            PermissionId = 5L,
                            RoleId = 2L
                        },
                        new
                        {
                            Id = 6L,
                            PermissionId = 6L,
                            RoleId = 2L
                        },
                        new
                        {
                            Id = 7L,
                            PermissionId = 7L,
                            RoleId = 2L
                        },
                        new
                        {
                            Id = 8L,
                            PermissionId = 8L,
                            RoleId = 2L
                        },
                        new
                        {
                            Id = 9L,
                            PermissionId = 9L,
                            RoleId = 2L
                        },
                        new
                        {
                            Id = 10L,
                            PermissionId = 10L,
                            RoleId = 2L
                        },
                        new
                        {
                            Id = 11L,
                            PermissionId = 11L,
                            RoleId = 2L
                        },
                        new
                        {
                            Id = 12L,
                            PermissionId = 12L,
                            RoleId = 2L
                        });
                });

            modelBuilder.Entity("Server.Common.Models.Urban", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("UrbanAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Urbans");
                });

            modelBuilder.Entity("Server.Common.Models.AccessToken", b =>
                {
                    b.HasOne("Server.Common.Models.RefreshToken", "RefreshToken")
                        .WithMany("AccessTokens")
                        .HasForeignKey("RtId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("Server.Common.Models.AccountGroup", b =>
                {
                    b.HasOne("Server.Common.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Common.Models.AccountModule", b =>
                {
                    b.HasOne("Server.Common.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Module", null)
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Common.Models.AccountPermission", b =>
                {
                    b.HasOne("Server.Common.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Common.Models.AccountRole", b =>
                {
                    b.HasOne("Server.Common.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Common.Models.Apartment", b =>
                {
                    b.HasOne("Server.Common.Models.Building", "Building")
                        .WithMany("Apartments")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Server.Common.Models.ApartmentDetail", b =>
                {
                    b.HasOne("Server.Common.Models.Apartment", "Apartment")
                        .WithOne("ApartmentDetail")
                        .HasForeignKey("Server.Common.Models.ApartmentDetail", "ApartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Apartment");
                });

            modelBuilder.Entity("Server.Common.Models.Building", b =>
                {
                    b.HasOne("Server.Common.Models.Module", "Module")
                        .WithMany("Buildings")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Urban", "Urban")
                        .WithMany("Buildings")
                        .HasForeignKey("UrbanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Urban");
                });

            modelBuilder.Entity("Server.Common.Models.Document", b =>
                {
                    b.HasOne("Server.Common.Models.Account", "Account")
                        .WithMany("Documents")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Module", "Module")
                        .WithMany("Documents")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Server.Common.Models.DocumentDetail", b =>
                {
                    b.HasOne("Server.Common.Models.Document", "Document")
                        .WithMany("DocumentDetails")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Document");
                });

            modelBuilder.Entity("Server.Common.Models.FormResidentRequest", b =>
                {
                    b.HasOne("Server.Common.Models.Account", "Account")
                        .WithMany("FormResidentRequests")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Module", "Module")
                        .WithMany("FormResidentRequests")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Server.Common.Models.FormResidentRequestDetail", b =>
                {
                    b.HasOne("Server.Common.Models.FormResidentRequest", "FormResidentRequest")
                        .WithMany("FormResidentRequestDetails")
                        .HasForeignKey("FormResidentRequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FormResidentRequest");
                });

            modelBuilder.Entity("Server.Common.Models.GroupPermission", b =>
                {
                    b.HasOne("Server.Common.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Common.Models.ModulePermission", b =>
                {
                    b.HasOne("Server.Common.Models.Module", null)
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Common.Models.RefreshToken", b =>
                {
                    b.HasOne("Server.Common.Models.Account", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Server.Common.Models.Resident", b =>
                {
                    b.HasOne("Server.Common.Models.Account", "Account")
                        .WithOne("Resident")
                        .HasForeignKey("Server.Common.Models.Resident", "AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Apartment", "Apartment")
                        .WithMany("Residents")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Module", "Module")
                        .WithMany("Residents")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Apartment");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("Server.Common.Models.RolePermission", b =>
                {
                    b.HasOne("Server.Common.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Common.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Server.Common.Models.Account", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("FormResidentRequests");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Resident");
                });

            modelBuilder.Entity("Server.Common.Models.Apartment", b =>
                {
                    b.Navigation("ApartmentDetail");

                    b.Navigation("Residents");
                });

            modelBuilder.Entity("Server.Common.Models.Building", b =>
                {
                    b.Navigation("Apartments");
                });

            modelBuilder.Entity("Server.Common.Models.Document", b =>
                {
                    b.Navigation("DocumentDetails");
                });

            modelBuilder.Entity("Server.Common.Models.FormResidentRequest", b =>
                {
                    b.Navigation("FormResidentRequestDetails");
                });

            modelBuilder.Entity("Server.Common.Models.Module", b =>
                {
                    b.Navigation("Buildings");

                    b.Navigation("Documents");

                    b.Navigation("FormResidentRequests");

                    b.Navigation("Residents");
                });

            modelBuilder.Entity("Server.Common.Models.RefreshToken", b =>
                {
                    b.Navigation("AccessTokens");
                });

            modelBuilder.Entity("Server.Common.Models.Urban", b =>
                {
                    b.Navigation("Buildings");
                });
#pragma warning restore 612, 618
        }
    }
}
