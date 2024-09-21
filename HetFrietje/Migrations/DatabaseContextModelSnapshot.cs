﻿// <auto-generated />
using System;
using HetFrietje.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HetFrietje.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesCategoryId", "ProductsProductId");

                    b.HasIndex("ProductsProductId");

                    b.ToTable("CategoryProduct");

                    b.HasData(
                        new
                        {
                            CategoriesCategoryId = 1,
                            ProductsProductId = 1
                        },
                        new
                        {
                            CategoriesCategoryId = 7,
                            ProductsProductId = 1
                        },
                        new
                        {
                            CategoriesCategoryId = 4,
                            ProductsProductId = 2
                        },
                        new
                        {
                            CategoriesCategoryId = 1,
                            ProductsProductId = 3
                        },
                        new
                        {
                            CategoriesCategoryId = 7,
                            ProductsProductId = 3
                        });
                });

            modelBuilder.Entity("HetFrietje.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Schotels"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Hamburgers"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Frieten"
                        },
                        new
                        {
                            CategoryId = 4,
                            Name = "Snacks"
                        },
                        new
                        {
                            CategoryId = 5,
                            Name = "Vega"
                        },
                        new
                        {
                            CategoryId = 6,
                            Name = "Frisdrank"
                        },
                        new
                        {
                            CategoryId = 7,
                            Name = "Aanbiedingen"
                        },
                        new
                        {
                            CategoryId = 8,
                            Name = "Overige"
                        });
                });

            modelBuilder.Entity("HetFrietje.Models.Option", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OptionId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Values")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OptionId");

                    b.ToTable("Options");

                    b.HasData(
                        new
                        {
                            OptionId = 1,
                            Name = "Maat",
                            Values = "[\"Small\",\"Medium\",\"Large\",\"Extra Large\"]"
                        },
                        new
                        {
                            OptionId = 2,
                            Name = "Saus",
                            Values = "[\"Knoflook\",\"Sambal\",\"Curry\",\"Mosterd\",\"Mayonaise\"]"
                        });
                });

            modelBuilder.Entity("HetFrietje.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("SubtotalPrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("Username");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            Status = 1,
                            SubtotalPrice = 123m,
                            TotalPrice = 123m,
                            Username = "Klant"
                        });
                });

            modelBuilder.Entity("HetFrietje.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal?>("SalesPrice")
                        .HasColumnType("decimal(7,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Description = "Voorbeeld product 1",
                            Name = "Product 1",
                            Price = 1.50m,
                            Stock = 152,
                            Tax = 9.00m
                        },
                        new
                        {
                            ProductId = 2,
                            Description = "Voorbeeld product 2",
                            Name = "Product 2",
                            Price = 7.45m,
                            Stock = 76,
                            Tax = 9.00m
                        },
                        new
                        {
                            ProductId = 3,
                            Description = "Voorbeeld product 3",
                            Name = "Product 3",
                            Price = 54.65m,
                            Stock = 26,
                            Tax = 9.00m
                        });
                });

            modelBuilder.Entity("HetFrietje.Models.ProductOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId", "ProductCount");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductOrders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            ProductId = 1,
                            ProductCount = 2
                        },
                        new
                        {
                            OrderId = 1,
                            ProductId = 2,
                            ProductCount = 1
                        });
                });

            modelBuilder.Entity("HetFrietje.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PermissionLevel")
                        .HasColumnType("int");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Username = "Klant",
                            Name = "Klant 1",
                            PermissionLevel = 1
                        });
                });

            modelBuilder.Entity("OptionProduct", b =>
                {
                    b.Property<int>("OptionsOptionId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsProductId")
                        .HasColumnType("int");

                    b.HasKey("OptionsOptionId", "ProductsProductId");

                    b.HasIndex("ProductsProductId");

                    b.ToTable("OptionProduct");

                    b.HasData(
                        new
                        {
                            OptionsOptionId = 1,
                            ProductsProductId = 1
                        },
                        new
                        {
                            OptionsOptionId = 2,
                            ProductsProductId = 1
                        },
                        new
                        {
                            OptionsOptionId = 1,
                            ProductsProductId = 2
                        },
                        new
                        {
                            OptionsOptionId = 2,
                            ProductsProductId = 2
                        },
                        new
                        {
                            OptionsOptionId = 1,
                            ProductsProductId = 3
                        });
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("HetFrietje.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HetFrietje.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HetFrietje.Models.Order", b =>
                {
                    b.HasOne("HetFrietje.Models.Product", null)
                        .WithMany("Orders")
                        .HasForeignKey("ProductId");

                    b.HasOne("HetFrietje.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HetFrietje.Models.ProductOrder", b =>
                {
                    b.HasOne("HetFrietje.Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HetFrietje.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OptionProduct", b =>
                {
                    b.HasOne("HetFrietje.Models.Option", null)
                        .WithMany()
                        .HasForeignKey("OptionsOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HetFrietje.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HetFrietje.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("HetFrietje.Models.Product", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HetFrietje.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
