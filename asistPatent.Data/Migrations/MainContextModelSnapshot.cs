﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using asistPatentCore.Data;

namespace asistPatentCore.Data.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("asistPatentCore.Model.DefaultValues", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("key")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("value")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("defaultValues");
                });

            modelBuilder.Entity("asistPatentCore.Model.MailSources", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("enableSsl")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("port")
                        .HasColumnType("int");

                    b.Property<string>("smtpServer")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("mailSources");
                });

            modelBuilder.Entity("asistPatentCore.Model.MailTemplates", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("mailContent")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("mailHeader")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("template")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("mailTemplates");
                });

            modelBuilder.Entity("asistPatentCore.Model.Users", b =>
                {
                    b.Property<Guid>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<DateTime>("userCreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("userEmailAdress")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("userName")
                        .HasColumnType("varchar(120) CHARACTER SET utf8mb4")
                        .HasMaxLength(120);

                    b.Property<string>("userPassword")
                        .HasColumnType("varchar(24) CHARACTER SET utf8mb4")
                        .HasMaxLength(24);

                    b.Property<string>("userPhoneNumber")
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11);

                    b.Property<string>("userSurname")
                        .HasColumnType("varchar(120) CHARACTER SET utf8mb4")
                        .HasMaxLength(120);

                    b.HasKey("userId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("asistPatentCore.Model.UsersToken", b =>
                {
                    b.Property<Guid>("tokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.Property<Guid>("userId")
                        .HasColumnType("char(36)");

                    b.HasKey("tokenId");

                    b.HasIndex("userId");

                    b.ToTable("usersTokens");
                });

            modelBuilder.Entity("asistPatentCore.Model.UsersToken", b =>
                {
                    b.HasOne("asistPatentCore.Model.Users", "Users")
                        .WithMany("UsersTokenRelation")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
