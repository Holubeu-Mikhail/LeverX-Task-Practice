use ProductsDb

INSERT [dbo].[Cities] ([Name], [Code]) VALUES (N'Minsk', CAST(242 AS Numeric(18, 0)))
INSERT [dbo].[Cities] ([Name], [Code]) VALUES (N'San Francisco', CAST(941 AS Numeric(18, 0)))
INSERT [dbo].[Cities] ([Name], [Code]) VALUES (N'Moscow', CAST(353 AS Numeric(18, 0)))

INSERT [dbo].[Brands] ([Name], [Description], [CityId]) VALUES (N'Milkavita', N'Products from milk', (SELECT TOP 1 Id FROM Cities WHERE Name = 'Minsk'))
INSERT [dbo].[Brands] ([Name], [Description], [CityId]) VALUES (N'Apple', N'Electronics manufacture', (SELECT TOP 1 Id FROM Cities WHERE Name = 'San Francisco'))
INSERT [dbo].[Brands] ([Name], [Description], [CityId]) VALUES (N'Luxardi', N'Build materials and other building staff', (SELECT TOP 1 Id FROM Cities WHERE Name = 'Moscow'))

INSERT [dbo].[ProductTypes] ([Name]) VALUES (N'Food')
INSERT [dbo].[ProductTypes] ([Name]) VALUES (N'Drinks')
INSERT [dbo].[ProductTypes] ([Name]) VALUES (N'Smartphones')
INSERT [dbo].[ProductTypes] ([Name]) VALUES (N'Build materials')

INSERT [dbo].[Products] ([Name], [Quantity], [TypeId], [BrandId]) VALUES (N'Milk', CAST(50 AS Numeric(18, 0)), (SELECT TOP 1 Id FROM ProductTypes WHERE Name = 'Drinks'), (SELECT TOP 1 Id FROM Brands WHERE Name = 'Milkavita'))
INSERT [dbo].[Products] ([Name], [Quantity], [TypeId], [BrandId]) VALUES (N'Cheese', CAST(100 AS Numeric(18, 0)), (SELECT TOP 1 Id FROM ProductTypes WHERE Name = 'Food'), (SELECT TOP 1 Id FROM Brands WHERE Name = 'Milkavita'))
INSERT [dbo].[Products] ([Name], [Quantity], [TypeId], [BrandId]) VALUES (N'iPhone 13', CAST(82 AS Numeric(18, 0)), (SELECT TOP 1 Id FROM ProductTypes WHERE Name = 'Smartphones'), (SELECT TOP 1 Id FROM Brands WHERE Name = 'Apple'))
INSERT [dbo].[Products] ([Name], [Quantity], [TypeId], [BrandId]) VALUES (N'Brick', CAST(1400 AS Numeric(18, 0)), (SELECT TOP 1 Id FROM ProductTypes WHERE Name = 'Build materials'), (SELECT TOP 1 Id FROM Brands WHERE Name = 'Luxardi'))
