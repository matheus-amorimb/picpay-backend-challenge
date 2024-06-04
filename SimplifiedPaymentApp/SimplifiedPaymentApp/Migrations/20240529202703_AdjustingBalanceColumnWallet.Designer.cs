﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SimplifiedPicPay.Context;

#nullable disable

namespace SimplifiedPicPay.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240529202703_AdjustingBalanceColumnWallet")]
    partial class AdjustingBalanceColumnWallet
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SimplifiedPaymentApp.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("transaction_id");

                    b.Property<Guid>("PayeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("payee_id");

                    b.Property<Guid>("PayerId")
                        .HasColumnType("uuid")
                        .HasColumnName("payer_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<float>("Value")
                        .HasColumnType("real")
                        .HasColumnName("value");

                    b.HasKey("TransactionId");

                    b.ToTable("transaction");
                });

            modelBuilder.Entity("SimplifiedPaymentApp.Models.Wallet", b =>
                {
                    b.Property<Guid>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("wallet_id");

                    b.Property<double>("Balance")
                        .HasColumnType("double precision")
                        .HasColumnName("balance");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<int>("WalletTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("wallet_type_id");

                    b.HasKey("WalletId");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("WalletTypeId");

                    b.ToTable("wallet");
                });

            modelBuilder.Entity("SimplifiedPaymentApp.Models.WalletType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("wallet_type_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.HasKey("Id");

                    b.ToTable("wallet_type");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "User"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Merchant"
                        });
                });

            modelBuilder.Entity("SimplifiedPaymentApp.Models.Wallet", b =>
                {
                    b.HasOne("SimplifiedPaymentApp.Models.WalletType", "WalletType")
                        .WithMany()
                        .HasForeignKey("WalletTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WalletType");
                });
#pragma warning restore 612, 618
        }
    }
}
