USE [master]
GO
/****** Object:  Database [HetFrietje]    Script Date: 12/10/2024 13:04:34 ******/
CREATE DATABASE [HetFrietje]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HetFrietje', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HetFrietje.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HetFrietje_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HetFrietje_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HetFrietje] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HetFrietje].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HetFrietje] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HetFrietje] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HetFrietje] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HetFrietje] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HetFrietje] SET ARITHABORT OFF 
GO
ALTER DATABASE [HetFrietje] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HetFrietje] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HetFrietje] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HetFrietje] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HetFrietje] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HetFrietje] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HetFrietje] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HetFrietje] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HetFrietje] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HetFrietje] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HetFrietje] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HetFrietje] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HetFrietje] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HetFrietje] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HetFrietje] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HetFrietje] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [HetFrietje] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HetFrietje] SET RECOVERY FULL 
GO
ALTER DATABASE [HetFrietje] SET  MULTI_USER 
GO
ALTER DATABASE [HetFrietje] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HetFrietje] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HetFrietje] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HetFrietje] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HetFrietje] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HetFrietje] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HetFrietje', N'ON'
GO
ALTER DATABASE [HetFrietje] SET QUERY_STORE = OFF
GO
USE [HetFrietje]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryProduct]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryProduct](
	[CategoriesCategoryId] [int] NOT NULL,
	[ProductsProductId] [int] NOT NULL,
 CONSTRAINT [PK_CategoryProduct] PRIMARY KEY CLUSTERED 
(
	[CategoriesCategoryId] ASC,
	[ProductsProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OptionProduct]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OptionProduct](
	[OptionsOptionId] [int] NOT NULL,
	[ProductsProductId] [int] NOT NULL,
 CONSTRAINT [PK_OptionProduct] PRIMARY KEY CLUSTERED 
(
	[OptionsOptionId] ASC,
	[ProductsProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Options]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Options](
	[OptionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Values] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Options] PRIMARY KEY CLUSTERED 
(
	[OptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](450) NULL,
	[Status] [int] NOT NULL,
	[TotalPrice] [decimal](10, 2) NOT NULL,
	[SubtotalPrice] [decimal](10, 2) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductOrders]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOrders](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductCount] [int] NOT NULL,
 CONSTRAINT [PK_ProductOrders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](7, 2) NOT NULL,
	[SalesPrice] [decimal](7, 2) NULL,
	[Tax] [decimal](5, 2) NOT NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/10/2024 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PermissionLevel] [int] NOT NULL,
	[password] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241010114258_initial', N'8.0.10')
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (1, N'Schotels')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (2, N'Hamburgers')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (3, N'Frieten')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (4, N'Snacks')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (5, N'Vega')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (6, N'Frisdrank')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (7, N'Aanbiedingen')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (8, N'Overige')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductId]) VALUES (2, 1)
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductId]) VALUES (1, 2)
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductId]) VALUES (2, 2)
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductId]) VALUES (3, 3)
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductId]) VALUES (7, 3)
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductId]) VALUES (2, 4)
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductId]) VALUES (7, 4)
INSERT [dbo].[CategoryProduct] ([CategoriesCategoryId], [ProductsProductId]) VALUES (8, 4)
GO
INSERT [dbo].[OptionProduct] ([OptionsOptionId], [ProductsProductId]) VALUES (1, 1)
INSERT [dbo].[OptionProduct] ([OptionsOptionId], [ProductsProductId]) VALUES (2, 1)
INSERT [dbo].[OptionProduct] ([OptionsOptionId], [ProductsProductId]) VALUES (1, 2)
INSERT [dbo].[OptionProduct] ([OptionsOptionId], [ProductsProductId]) VALUES (2, 2)
INSERT [dbo].[OptionProduct] ([OptionsOptionId], [ProductsProductId]) VALUES (1, 3)
INSERT [dbo].[OptionProduct] ([OptionsOptionId], [ProductsProductId]) VALUES (1, 4)
GO
SET IDENTITY_INSERT [dbo].[Options] ON 

INSERT [dbo].[Options] ([OptionId], [Name], [Values]) VALUES (1, N'Maat', N'["Small","Medium","Large","Extra Large"]')
INSERT [dbo].[Options] ([OptionId], [Name], [Values]) VALUES (2, N'Saus', N'["Knoflook","Sambal","Curry","Mosterd","Mayonaise"]')
SET IDENTITY_INSERT [dbo].[Options] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [Username], [Status], [TotalPrice], [SubtotalPrice], [OrderDate]) VALUES (1, N'Klant', 2, CAST(123.00 AS Decimal(10, 2)), CAST(123.00 AS Decimal(10, 2)), CAST(N'2024-10-10T13:42:57.6827064' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[ProductOrders] ([OrderId], [ProductId], [ProductCount]) VALUES (1, 1, 2)
INSERT [dbo].[ProductOrders] ([OrderId], [ProductId], [ProductCount]) VALUES (1, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [SalesPrice], [Tax], [Stock]) VALUES (1, N'Cheeseburger', N'Klasieke cheeseburger zoals iedereen hem kent.', CAST(1.50 AS Decimal(7, 2)), NULL, CAST(9.00 AS Decimal(5, 2)), 32)
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [SalesPrice], [Tax], [Stock]) VALUES (2, N'Spekburger schotel', N'De burger.. met spek. maar dan in schotel variant?', CAST(7.45 AS Decimal(7, 2)), NULL, CAST(9.00 AS Decimal(5, 2)), 76)
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [SalesPrice], [Tax], [Stock]) VALUES (3, N'Friet', N'Onze beste friet, gemaakt van 5% aardappelen en 95% zout.', CAST(54.65 AS Decimal(7, 2)), CAST(12.00 AS Decimal(7, 2)), CAST(9.00 AS Decimal(5, 2)), 26)
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [SalesPrice], [Tax], [Stock]) VALUES (4, N'Superburger', N'De geweldige super burger, met 0 % natuurlijk vlees.', CAST(12.65 AS Decimal(7, 2)), CAST(12.00 AS Decimal(7, 2)), CAST(9.00 AS Decimal(5, 2)), 18)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
INSERT [dbo].[Users] ([Username], [Name], [PermissionLevel], [password]) VALUES (N'Klant', N'Klant 1', 1, NULL)
GO
/****** Object:  Index [IX_CategoryProduct_ProductsProductId]    Script Date: 12/10/2024 13:04:34 ******/
CREATE NONCLUSTERED INDEX [IX_CategoryProduct_ProductsProductId] ON [dbo].[CategoryProduct]
(
	[ProductsProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OptionProduct_ProductsProductId]    Script Date: 12/10/2024 13:04:34 ******/
CREATE NONCLUSTERED INDEX [IX_OptionProduct_ProductsProductId] ON [dbo].[OptionProduct]
(
	[ProductsProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Orders_Username]    Script Date: 12/10/2024 13:04:34 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_Username] ON [dbo].[Orders]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductOrders_ProductId]    Script Date: 12/10/2024 13:04:34 ******/
CREATE NONCLUSTERED INDEX [IX_ProductOrders_ProductId] ON [dbo].[ProductOrders]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoryProduct]  WITH CHECK ADD  CONSTRAINT [FK_CategoryProduct_Categories_CategoriesCategoryId] FOREIGN KEY([CategoriesCategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryProduct] CHECK CONSTRAINT [FK_CategoryProduct_Categories_CategoriesCategoryId]
GO
ALTER TABLE [dbo].[CategoryProduct]  WITH CHECK ADD  CONSTRAINT [FK_CategoryProduct_Products_ProductsProductId] FOREIGN KEY([ProductsProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryProduct] CHECK CONSTRAINT [FK_CategoryProduct_Products_ProductsProductId]
GO
ALTER TABLE [dbo].[OptionProduct]  WITH CHECK ADD  CONSTRAINT [FK_OptionProduct_Options_OptionsOptionId] FOREIGN KEY([OptionsOptionId])
REFERENCES [dbo].[Options] ([OptionId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OptionProduct] CHECK CONSTRAINT [FK_OptionProduct_Options_OptionsOptionId]
GO
ALTER TABLE [dbo].[OptionProduct]  WITH CHECK ADD  CONSTRAINT [FK_OptionProduct_Products_ProductsProductId] FOREIGN KEY([ProductsProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OptionProduct] CHECK CONSTRAINT [FK_OptionProduct_Products_ProductsProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_Username] FOREIGN KEY([Username])
REFERENCES [dbo].[Users] ([Username])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_Username]
GO
ALTER TABLE [dbo].[ProductOrders]  WITH CHECK ADD  CONSTRAINT [FK_ProductOrders_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductOrders] CHECK CONSTRAINT [FK_ProductOrders_Orders_OrderId]
GO
ALTER TABLE [dbo].[ProductOrders]  WITH CHECK ADD  CONSTRAINT [FK_ProductOrders_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductOrders] CHECK CONSTRAINT [FK_ProductOrders_Products_ProductId]
GO
USE [master]
GO
ALTER DATABASE [HetFrietje] SET  READ_WRITE 
GO
