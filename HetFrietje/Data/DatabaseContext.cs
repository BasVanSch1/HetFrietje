using HetFrietje.Models;
using Microsoft.EntityFrameworkCore;

namespace HetFrietje.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
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
                .WithOne(o => o.User)
                .HasForeignKey(o => o.Username); // Order.User koppelen aan Order.Username

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(7,2)"); // 7 cijfers waarvan 2 achter de komma

            modelBuilder.Entity<Product>()
                .Property(p => p.SalesPrice)
                .HasColumnType("decimal(7,2)"); // 7 cijfers waarvan 2 achter de komma

            modelBuilder.Entity<Product>()
                .Property(p => p.Tax)
                .HasColumnType("decimal(5,2)"); // 5 cijfers waarvan 2 achter de komma

            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.OrderId, po.ProductId }); // koppeltabel tussen product en order met een productaantal erbij

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Order)
                .WithMany(o => o.Products);

            modelBuilder.Entity<ProductOrder>()
                .HasOne<Product>(po => po.Product)
                .WithMany(p => p.Orders);

            modelBuilder.Entity<Product>()
                .HasMany<Option>(p => p.Options)
                .WithMany(o => o.Products)
                .UsingEntity(join =>
                    join.HasData(new[] {
                            new { ProductsProductId = 1, OptionsOptionId = 1},
                            new { ProductsProductId = 1, OptionsOptionId = 2},
                            new { ProductsProductId = 2, OptionsOptionId = 1},
                            new { ProductsProductId = 2, OptionsOptionId = 2},
                            new { ProductsProductId = 3, OptionsOptionId = 1},
                            new { ProductsProductId = 4, OptionsOptionId = 1}
                        })
                    );

            modelBuilder.Entity<Product>()
                .HasMany<Category>(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity(join =>
                    join.HasData(new[] {
                        new { ProductsProductId = 1, CategoriesCategoryId = 2},
                        new { ProductsProductId = 2, CategoriesCategoryId = 1},
                        new { ProductsProductId = 2, CategoriesCategoryId = 2},
                        new { ProductsProductId = 3, CategoriesCategoryId = 3},
                        new { ProductsProductId = 3, CategoriesCategoryId = 7},
                        new { ProductsProductId = 4, CategoriesCategoryId = 2},
                        new { ProductsProductId = 4, CategoriesCategoryId = 7},
                        new { ProductsProductId = 4, CategoriesCategoryId = 8}
                    })
                );

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.SubtotalPrice)
                .HasColumnType("decimal(10,2)");

            // Dummy data
            // producten aanmaken
            Product product1 = new Product()
            {
                ProductId = 1,
                Name = "Cheeseburger",
                Description = "Klasieke cheeseburger zoals iedereen hem kent.",
                Price = 1.50m,
                Tax = 9.00m,
                Stock = 32
            };

            Product product2 = new Product()
            {
                ProductId = 2,
                Name = "Spekburger schotel",
                Description = "De burger.. met spek. maar dan in schotel variant?",
                Price = 7.45m,
                Tax = 9.00m,
                Stock = 76
            };

            Product product3 = new Product()
            {
                ProductId = 3,
                Name = "Friet",
                Description = "Onze beste friet, gemaakt van 5% aardappelen en 95% zout.",
                Price = 54.65m,
                SalesPrice = 12m,
                Tax = 9.00m,
                Stock = 26
            };

            Product product4 = new Product()
            {
                ProductId = 4,
                Name = "Superburger",
                Description = "De geweldige super burger, met 0 % natuurlijk vlees.",
                Price = 12.65m,
                SalesPrice = 12m,
                Tax = 9.00m,
                Stock = 18
            };

            
            modelBuilder.Entity<Product>()
                .HasData(product1);
            modelBuilder.Entity<Product>()
                .HasData(product2);
            modelBuilder.Entity<Product>()
                .HasData(product3);
            modelBuilder.Entity<Product>()
                .HasData(product4);

            // opties aanmaken
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

            // users aanmaken
            User klant = new User()
            {
                Username = "Klant",
                Name = "Klant 1",
                PermissionLevel = PermissionLevel.CUSTOMER
            };

            modelBuilder.Entity<User>()
                .HasData(klant);

            // orders aanmaken
            Order order = new Order()
            {
                OrderId = 1,
                Username = "Klant",
                Status = OrderStatus.CONFIRMED,
                TotalPrice = 123,
                SubtotalPrice = 123,
                OrderDate = DateTime.Now,
            };

            modelBuilder.Entity<Order>()
                .HasData(order);

            // producten koppelen aan orders
            ProductOrder productOrder1 = new ProductOrder()
            {
                OrderId = 1,
                ProductId = 1,
                ProductCount = 2
            };

            ProductOrder productOrder2 = new ProductOrder()
            {
                OrderId = 1,
                ProductId = 2,
                ProductCount = 1
            };

            modelBuilder.Entity<ProductOrder>()
                .HasData(productOrder1);
            modelBuilder.Entity<ProductOrder>()
                .HasData(productOrder2);
        }
    }
}
