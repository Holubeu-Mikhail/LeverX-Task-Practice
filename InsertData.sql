use ProductsDb

INSERT [dbo].[Towns] ([Id], [Name], [Code]) VALUES (CAST(1 AS Numeric(18, 0)), N'Minsk', CAST(242 AS Numeric(18, 0)))
INSERT [dbo].[Towns] ([Id], [Name], [Code]) VALUES (CAST(2 AS Numeric(18, 0)), N'San Francisco', CAST(941 AS Numeric(18, 0)))
INSERT [dbo].[Towns] ([Id], [Name], [Code]) VALUES (CAST(3 AS Numeric(18, 0)), N'Moscow', CAST(353 AS Numeric(18, 0)))

INSERT [dbo].[Brands] ([Id], [Name], [Description], [TownId]) VALUES (CAST(1 AS Numeric(18, 0)), N'Milkavita', N'Products from milk', CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [TownId]) VALUES (CAST(2 AS Numeric(18, 0)), N'Apple', N'Electronics manufacture', CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[Brands] ([Id], [Name], [Description], [TownId]) VALUES (CAST(3 AS Numeric(18, 0)), N'Luxardi', N'Build materials and other building staff', CAST(3 AS Numeric(18, 0)))

INSERT [dbo].[ProductTypes] ([Id], [Name]) VALUES (CAST(1 AS Numeric(18, 0)), N'Food')
INSERT [dbo].[ProductTypes] ([Id], [Name]) VALUES (CAST(2 AS Numeric(18, 0)), N'Drinks')
INSERT [dbo].[ProductTypes] ([Id], [Name]) VALUES (CAST(3 AS Numeric(18, 0)), N'Smartphones')
INSERT [dbo].[ProductTypes] ([Id], [Name]) VALUES (CAST(4 AS Numeric(18, 0)), N'Build materials')

INSERT [dbo].[Products] ([Id], [Name], [Quantity], [TypeId], [BrandId]) VALUES (CAST(1 AS Numeric(18, 0)), N'Milk', CAST(50 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[Products] ([Id], [Name], [Quantity], [TypeId], [BrandId]) VALUES (CAST(2 AS Numeric(18, 0)), N'Cheese', CAST(100 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[Products] ([Id], [Name], [Quantity], [TypeId], [BrandId]) VALUES (CAST(3 AS Numeric(18, 0)), N'iPhone 13', CAST(82 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[Products] ([Id], [Name], [Quantity], [TypeId], [BrandId]) VALUES (CAST(4 AS Numeric(18, 0)), N'Brick', CAST(1400 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)))