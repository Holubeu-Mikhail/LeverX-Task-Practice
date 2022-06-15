use ProductsDb

INSERT [dbo].[ProductTypes] ([Id], [Name]) VALUES (CAST(1 AS Numeric(18, 0)), N'Food')
INSERT [dbo].[ProductTypes] ([Id], [Name]) VALUES (CAST(2 AS Numeric(18, 0)), N'Drinks')

INSERT [dbo].[Products] ([Id], [Name], [Quantity], [TypeId]) VALUES (CAST(1 AS Numeric(18, 0)), N'Salad', CAST(25 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[Products] ([Id], [Name], [Quantity], [TypeId]) VALUES (CAST(2 AS Numeric(18, 0)), N'Cheese', CAST(19 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[Products] ([Id], [Name], [Quantity], [TypeId]) VALUES (CAST(3 AS Numeric(18, 0)), N'Coffee', CAST(36 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[Products] ([Id], [Name], [Quantity], [TypeId]) VALUES (CAST(4 AS Numeric(18, 0)), N'Tea', CAST(14 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)))