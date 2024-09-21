using HetFrietje.Models;
using Microsoft.EntityFrameworkCore;

namespace HetFrietje.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionstring = @"Data Source=.;Initial Catalog=HetFrietje;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";
            optionsBuilder.UseSqlServer(connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property<String>("password"); // shadow property, dit wordt niet in de class opgeslagen, alleen maar in de database.

            modelBuilder.Entity<User>()
                .HasKey(u => u.Username); // primary key aanpassen

            modelBuilder.Entity<User>()
                .HasMany<Order>(u => u.Orders)
                .WithOne(o => o.User);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(7,2)"); // 7 cijfers waarvan 2 achter de komma

            modelBuilder.Entity<Product>()
                .Property(p => p.SalesPrice)
                .HasColumnType("decimal(7,2)"); // 7 cijfers waarvan 2 achter de komma

            modelBuilder.Entity<Product>()
                .Property(p => p.Tax)
                .HasColumnType("decimal(5,2)"); // 5 cijfers waarvan 2 achter de komma

            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId }); // primary key voor de koppeltabel

            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.OrderId, po.ProductId }); // primary key voor de koppeltabel

            modelBuilder.Entity<Product>()
                .HasMany<Option>(p => p.Options)
                .WithMany(o => o.Products)
                .UsingEntity(join =>
                    join.HasData(new[] // data invoeren, omdat er al meteen data wordt ingevoerd hoef je niet per sé .ToTable("") uit te voeren.
                    {                  // deze wordt automatisch aangemaakt.
                        new { ProductsProductId = 1, OptionsOptionId = 1},
                        new { ProductsProductId = 1, OptionsOptionId = 2 },
                        new { ProductsProductId = 2, OptionsOptionId = 1 },
                        new { ProductsProductId = 3, OptionsOptionId = 1 },
                        new { ProductsProductId = 3, OptionsOptionId = 2 }
                    })
                );

            //modelBuilder.Entity<ProductOption>()
            //    .HasKey(po => new { po.ProductId, po.OptionId }); // primary key voor de koppeltabel

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.SubtotalPrice)
                .HasColumnType("decimal(10,2)");


            // Dummy data
            // maak producten aan
            Product product1 = new Product()
            {
                ProductId = 1,
                Name = "Product 1",
                Description = "Voorbeeld product 1",
                Price = 1.50m,
                Tax = 9.00m,
                Stock = 152
            };

            Product product2 = new Product()
            {
                ProductId = 2,
                Name = "Product 2",
                Description = "Voorbeeld product 2",
                Price = 7.45m,
                Tax = 9.00m,
                Stock = 76
            };

            Product product3 = new Product()
            {
                ProductId = 3,
                Name = "Product 3",
                Description = "Voorbeeld product 3",
                Price = 54.65m,
                Tax = 9.00m,
                Stock = 26
            };

            modelBuilder.Entity<Product>()
                .HasData(product1);
            modelBuilder.Entity<Product>()
                .HasData(product2);
            modelBuilder.Entity<Product>()
                .HasData(product3);

            // maak opties aan
            Option option1 = new Option()
            {
                OptionId = 1,
                Name = "Maat",
                Values = ["Small", "Medium", "Large", "Extra Large"]
            };

            Option option2 = new Option()
            {
                OptionId = 2,
                Name = "Saus",
                Values = ["Knoflook", "Sambal", "Curry", "Mosterd", "Mayonaise"]
            };

            modelBuilder.Entity<Option>()
                .HasData(option1);
            modelBuilder.Entity<Option>()
                .HasData(option2);

            // koppel opties aan product
            //ProductOption productOption1 = new ProductOption()
            //{
            //    ProductId = 1,
            //    OptionId = 1
            //};
            //ProductOption productOption2 = new ProductOption()
            //{
            //    ProductId = 1,
            //    OptionId = 2
            //};

            //ProductOption productOption3 = new ProductOption()
            //{
            //    ProductId = 2,
            //    OptionId = 1
            //};

            //ProductOption productOption4 = new ProductOption()
            //{
            //    ProductId = 3,
            //    OptionId = 1
            //};

            //ProductOption productOption5 = new ProductOption()
            //{
            //    ProductId = 3,
            //    OptionId = 2
            //};

            //modelBuilder.Entity<ProductOption>()
            //    .HasData(productOption1);
            //modelBuilder.Entity<ProductOption>()
            //    .HasData(productOption2);
            //modelBuilder.Entity<ProductOption>()
            //    .HasData(productOption3);
            //modelBuilder.Entity<ProductOption>()
            //    .HasData(productOption4);
            //modelBuilder.Entity<ProductOption>()
            //    .HasData(productOption5);

            // categorieën aanmaken
            Category category1 = new Category()
            {
                CategoryId = 1,
                Name = "Schotels"
            };
            Category category2 = new Category()
            {
                CategoryId = 2,
                Name = "Hamburgers"
            };
            Category category3 = new Category()
            {
                CategoryId = 3,
                Name = "Frieten"
            };
            Category category4 = new Category()
            {
                CategoryId = 4,
                Name = "Snacks"
            };
            Category category5 = new Category()
            {
                CategoryId= 5,
                Name = "Vega"
            };
            Category category6 = new Category()
            {
                CategoryId = 6,
                Name = "Frisdrank"
            };
            Category category7 = new Category()
            {
                CategoryId = 7,
                Name = "Aanbiedingen"
            };
            Category category8 = new Category()
            {
                CategoryId = 8,
                Name = "Overige"
            };

            modelBuilder.Entity<Category>()
                .HasData(category1);
            modelBuilder.Entity<Category>()
                .HasData(category2);
            modelBuilder.Entity<Category>()
                .HasData(category3);
            modelBuilder.Entity<Category>()
                .HasData(category4);
            modelBuilder.Entity<Category>()
                .HasData(category5);
            modelBuilder.Entity<Category>()
                .HasData(category6);
            modelBuilder.Entity<Category>()
                .HasData(category7);
            modelBuilder.Entity<Category>()
                .HasData(category8);

            // categorieën koppelen aan producten
            ProductCategory productCategory1 = new ProductCategory()
            {
                CategoryId = 1,
                ProductId = 1
            };

            ProductCategory productCategory2 = new ProductCategory()
            {
                CategoryId = 7,
                ProductId = 1
            };

            ProductCategory productCategory3 = new ProductCategory()
            {
                CategoryId = 4,
                ProductId = 2
            };

            ProductCategory productCategory4 = new ProductCategory()
            {
                CategoryId = 1,
                ProductId = 3
            };

            ProductCategory productCategory5 = new ProductCategory()
            {
                CategoryId = 7,
                ProductId = 3
            };

            modelBuilder.Entity<ProductCategory>()
                .HasData(productCategory1);
            modelBuilder.Entity<ProductCategory>()
                .HasData(productCategory2);
            modelBuilder.Entity<ProductCategory>()
                .HasData(productCategory3);
            modelBuilder.Entity<ProductCategory>()
                .HasData(productCategory4);
            modelBuilder.Entity<ProductCategory>()
                .HasData(productCategory5);
        }
    }
}
