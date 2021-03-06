USE master
go
CREATE DATABASE [DBWeSplit]
GO
USE [DBWeSplit]
GO
/****** Object:  FullTextCatalog [chuyendi_cltg]    Script Date: 17/12/2020 18:12:11 ******/
CREATE FULLTEXT CATALOG [chuyendi_cltg] WITH ACCENT_SENSITIVITY = OFF
AS DEFAULT
GO
/****** Object:  Table [dbo].[ChuyenDi]    Script Date: 17/12/2020 18:12:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuyenDi](
	[IDChuyenDi] [int] IDENTITY(1,1) NOT NULL,
	[TenChuyenDi] [nvarchar](50) NOT NULL,
	[NgayBatDau] [datetime] NOT NULL,
	[NgayKetThuc] [datetime] NOT NULL,
	[GioiThieu] [ntext] NULL,
	[LoTrinh] [ntext] NULL,
	[HinhAnh] [nvarchar](max) NULL,
	[Leader] [int] NOT NULL,
 CONSTRAINT [PK_ChuyenDi] PRIMARY KEY CLUSTERED 
(
	[IDChuyenDi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HinhAnhChuyenDi]    Script Date: 17/12/2020 18:12:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HinhAnhChuyenDi](
	[IDHinhAnh] [int] IDENTITY(1,1) NOT NULL,
	[IDChuyenDi] [int] NOT NULL,
	[HinhAnh] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_HinhAnhChuyenDi] PRIMARY KEY CLUSTERED 
(
	[IDHinhAnh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhoanChiTieu]    Script Date: 17/12/2020 18:12:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoanChiTieu](
	[IDKhoanChi] [int] IDENTITY(1,1) NOT NULL,
	[IDChuyenDi] [int] NOT NULL,
	[TenKhoanChi] [nvarchar](50) NOT NULL,
	[SoTien] [money] NOT NULL,
 CONSTRAINT [PK_KhoanChiTieu] PRIMARY KEY CLUSTERED 
(
	[IDKhoanChi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhoanThu]    Script Date: 17/12/2020 18:12:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoanThu](
	[IDChuyenDi] [int] NOT NULL,
	[IDNguoiDongTien] [int] NOT NULL,
	[SoTien] [money] NOT NULL,
 CONSTRAINT [PK_KhoanThu] PRIMARY KEY CLUSTERED 
(
	[IDChuyenDi] ASC,
	[IDNguoiDongTien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThamGiaChuyenDi]    Script Date: 17/12/2020 18:12:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThamGiaChuyenDi](
	[IDChuyenDi] [int] NOT NULL,
	[IDThanhVien] [int] NOT NULL,
 CONSTRAINT [PK_ThamGiaChuyenDi] PRIMARY KEY CLUSTERED 
(
	[IDChuyenDi] ASC,
	[IDThanhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThanhVien]    Script Date: 17/12/2020 18:12:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThanhVien](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenThanhVien] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ThanhVien] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChuyenDi] ON 

INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (1, N'Du Lịch Vũng Tàu', CAST(N'2020-10-25T00:00:00.000' AS DateTime), CAST(N'2020-10-27T00:00:00.000' AS DateTime), N'Vũng Tàu là một thành phố biển có 42 km bờ biển bao quanh, có núi Lớn (núi Tương Kỳ) cao 245 m và núi Nhỏ (núi Tao Phùng) cao 170 m. Trên núi Nhỏ có ngọn hải đăng cao 18 m, chiếu xa tới 30 hải lý và có tuổi đời trên 100 năm, được coi là ngọn hải đăng lâu đời nhất Việt Nam.', N'06h30 sáng ngày 25, khởi hành bằng xe máy đi từ Thủ Ðức ra Vũng Tàu. 
10h30 Ra tới Biển Vũng Tàu. 
12h30 Vào đặt phòng khách sạn.
14h30 đi đến nhà treo ngược.
17h quay trở lại biển và nhậu hải sản nướng.
20h về lại khách sạn.
8h sáng ngày 26 đi lặn biển. 
11h di an hải sản ở nhà hàng. 
13h quay về khách sạn. 
15h đi câu mực. 
20h về lại khách sạn.
7h30 sáng ngày 27, khởi hành về nhà.', N'Images/VungTau.jpg', 1)
INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (2, N'Phượt vịnh Lan Hạ', CAST(N'2020-11-20T00:00:00.000' AS DateTime), CAST(N'2020-11-23T00:00:00.000' AS DateTime), N'Vịnh Lan Hạ được ví với một dải lụa yêu kiều mà được dệt bởi nước biển trong xanh đến tận đáy và thảm thực vật xanh mướt mát. Diện tích của vịnh rộng trên 7.000 ha, tuy nhiên trong đó có hơn 5.400 ha là thuộc quyền quản lý của khu dự trữ sinh quyển được UNESCO công nhận – Vườn Quốc gia Cát Bà.', NULL, N'Images/VinhLanHa.jpg', 2)
INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (3, N'Khám phá Đà Nẵng', CAST(N'2020-12-24T00:00:00.000' AS DateTime), CAST(N'2020-12-27T00:00:00.000' AS DateTime), N'Đà Nẵng là một thành phố trực thuộc trung ương, nằm trong vùng Duyên hải Nam Trung Bộ Việt Nam, là thành phố trung tâm và lớn nhất khu vực miền Trung - Tây Nguyên.', NULL, N'Images/DaNang.jpg', 3)
INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (4, N'Đảo Phú Quốc', CAST(N'2021-01-23T00:00:00.000' AS DateTime), CAST(N'2021-01-24T00:00:00.000' AS DateTime), N'Phú Quốc, còn được mệnh danh là Đảo Ngọc, là hòn đảo lớn nhất Việt Nam, nằm trong vịnh Thái Lan. Đảo Phú Quốc cùng với các đảo nhỏ hơn ở lân cận và quần đảo Thổ Chu nằm cách đó 55 hải lý về phía tây nam hợp thành thành phố Phú Quốc thuộc tỉnh Kiên Giang. Đây cũng là thành phố đảo đầu tiên của Việt Nam.', NULL, N'Images/DaoPhuQuoc.jpg', 4)
INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (5, N'Thăm động Phong Nha - Kẻ Bàng', CAST(N'2021-02-25T00:00:00.000' AS DateTime), CAST(N'2021-02-28T00:00:00.000' AS DateTime), N'Vườn quốc gia Phong Nha – Kẻ Bàng là một vườn quốc gia của Việt Nam, nằm tại huyện Bố Trạch và Minh Hóa, tỉnh Quảng Bình, cách thành phố Đồng Hới khoảng 50 km về phía Tây Bắc, cách thủ đô Hà Nội khoảng 500 km về phía nam.', NULL, N'Images/DongPhongNha.jpg', 5)
INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (9, N'Phượt Hà Giang, những ngọn núi hùng vĩ', CAST(N'2021-03-17T00:00:00.000' AS DateTime), CAST(N'2021-03-19T00:00:00.000' AS DateTime), N'Hà Giang trong góc nhìn của tôi là một thử thách, trải nghiệm mà bất kỳ bạn trẻ nào phải thực hiện nó ít nhất một lần trong đời. Vượt qua những chặn đường gian treo leo vách núi, khuôn mặt sạm đi vì nắng vì gió là một cảm giác chinh phục được mọi khó khăn, vượt qua được giới hạn của bản thân và hòa quện tâm hồn cảm xúc với thiên nhiên đẹp mê lòng người ở nơi đây', NULL, N'Images/HaGiang.jpg', 2)
INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (10, N'Du lịch Phú Yên theo một cách hoàn toàn mới!', CAST(N'2021-04-20T00:00:00.000' AS DateTime), CAST(N'2021-04-23T00:00:00.000' AS DateTime), N'Phú Yên đẹp tựa một cô thôn nữ, có nét chân chất, mộc mạc của làng quê Việt Nam lại ẩn chứa những nét nhẹ nhàng, thuần khiết, trong trẻo. Vẻ đẹp của Phú Yên đi cùng với những câu hát tuổi thơ “Bóng trăng trắng ngà, có cây đa to, có thằng Cuội già, ôm một mối mơ” trong bộ phim “Tôi thấy hoa vàng trên cỏ xanh” do đạo diễn Victor Vũ chắp tay thực hiện, đã làm lay động trái tim của biết bao nhiêu khán giả. Cũng chính bởi lẽ đó, du lịch Phú Yên đã trở thành một điểm đến lí tưởng trong lòng rất nhiều du khách!', NULL, N'Images/PhuYen.jpg', 6)
INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (11, N'Thăm quan Huế', CAST(N'2021-05-26T00:00:00.000' AS DateTime), CAST(N'2021-05-30T00:00:00.000' AS DateTime), N'Huế bao đời nay vẫn luôn gắn liền với vẻ mộng mơ, xưa kính của vùng đất kinh kỳ, dù giờ đây đã trở thành một trong những thành phố trung tâm của Việt Nam. Nếu bạn bỗng dưng muốn rời khỏi phố thị ồn ã, hãy lưu ngay những kinh nghiệm du lịch Huế tự túc dưới đây để tận hưởng nhịp sống đậm chất kinh kỳ trên từng ngõ phố.', NULL, N'Images/Hue.jpg', 7)
INSERT [dbo].[ChuyenDi] ([IDChuyenDi], [TenChuyenDi], [NgayBatDau], [NgayKetThuc], [GioiThieu], [LoTrinh], [HinhAnh], [Leader]) VALUES (12, N'Đi chùa Ông Núi', CAST(N'2020-12-17T00:00:00.000' AS DateTime), CAST(N'2020-12-18T00:00:00.000' AS DateTime), N'Đến Bình Định, bên cạnh những bãi biển xinh đẹp nổi tiếng thì một địa danh mà du khách không nên bỏ qua chính là chùa Ông Núi – ngôi chùa cổ và nổi tiếng nhất ở đây. Trong đó điểm nổi bật nhất của chùa chính là bức tượng Phật ngồi lớn nhất Đông Nam Á mới được khánh thành vào đầu năm nay.', N'', N'Images/tuong-phat-chua-ong-nui-binh-dinh-6.jpg', 1)
SET IDENTITY_INSERT [dbo].[ChuyenDi] OFF
GO
SET IDENTITY_INSERT [dbo].[HinhAnhChuyenDi] ON 

INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (1, 1, N'Images/VungTau01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (2, 1, N'Images/VungTau02.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (3, 1, N'Images/VungTau03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (4, 1, N'Images/VungTau04.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (5, 2, N'Images/LanHa01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (6, 2, N'Images/LanHa02.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (7, 2, N'Images/LanHa03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (9, 2, N'Images/LanHa03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (10, 2, N'Images/LanHa01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (11, 2, N'Images/LanHa04.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (12, 3, N'Images/DaNang01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (13, 3, N'Images/DaNang02.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (14, 3, N'Images/DaNang03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (15, 3, N'Images/DaNang04.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (16, 3, N'Images/DaNang01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (17, 3, N'Images/DaNang05.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (18, 4, N'Images/DaoPhuQuoc01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (19, 4, N'Images/DaoPhuQuoc02.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (20, 4, N'Images/DaoPhuQuoc03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (21, 4, N'Images/DaoPhuQuoc04.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (22, 5, N'Images/DongPhongNha01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (23, 5, N'Images/DongPhongNha02.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (24, 5, N'Images/DongPhongNha03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (25, 5, N'Images/DongPhongNha04.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (26, 9, N'Images/HaGiang01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (27, 9, N'Images/HaGiang02.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (28, 9, N'Images/HaGiang03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (29, 9, N'Images/HaGiang04.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (30, 10, N'Images/PhuYen01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (31, 10, N'Images/PhuYen02.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (32, 10, N'Images/PhuYen03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (33, 10, N'Images/PhuYen04.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (34, 11, N'Images/Images/PhuYen01.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (35, 11, N'Images/Images/PhuYen02.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (36, 11, N'Images/Images/PhuYen03.jpg')
INSERT [dbo].[HinhAnhChuyenDi] ([IDHinhAnh], [IDChuyenDi], [HinhAnh]) VALUES (37, 11, N'Images/Images/PhuYen04.jpg')
SET IDENTITY_INSERT [dbo].[HinhAnhChuyenDi] OFF
GO
SET IDENTITY_INSERT [dbo].[KhoanChiTieu] ON 

INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (1, 1, N'Mua đồ nướng', 300000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (2, 1, N'Thuê Khách Sạn', 500000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (3, 1, N'Ăn nhà hàng', 400000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (4, 1, N'Thuê đồ lặn', 300000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (5, 1, N'Mua bia', 600000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (6, 2, N'Vé oto và tàu cao tốc Hà Nội – Bến Bèo 2 chiều', 4680000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (7, 2, N'Cano cao tốc Bến Bèo - Bè Vạn Bội 2 chiều', 1350000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (9, 2, N'Vé thắng cảnh Vịnh Lan Hạ', 3600000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (10, 2, N'Ăn uống', 450000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (11, 2, N'Thuê kayak', 450000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (12, 2, N'Khách sạn', 1080000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (13, 2, N'Chi phí khác (nước suối, găng tay, hoa quả)', 360000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (14, 3, N'Ăn uống', 1800000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (15, 3, N'Vé tham quan Bà Nà Hill', 3500000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (17, 3, N'Khách sạn', 3000000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (18, 3, N'Chi phí khác (nước suối, găng tay, hoa quả)', 360000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (19, 4, N'Ăn uống', 1800000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (20, 4, N'Thuê ca nô ra biển', 800000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (21, 4, N'Khách sạn', 3000000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (25, 4, N'Chi phí khác (nước suối, găng tay, hoa quả)', 360000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (26, 5, N'Ăn uống', 1800000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (27, 5, N'Khách sạn', 3000000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (28, 5, N'Vui chơi', 450000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (29, 9, N'Ăn uống', 450000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (30, 9, N'Tiền xăng đi lại', 500000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (31, 9, N'Khách sạn', 1500000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (32, 9, N'Chi phí khác (nước suối, găng tay, hoa quả)', 360000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (33, 10, N'Ăn uống', 450000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (34, 10, N'Khách sạn', 3000000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (35, 10, N'Vui chơi', 500000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (36, 10, N'Chi phí khác (nước suối, găng tay, hoa quả)', 360000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (37, 11, N'Ăn uống', 1000000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (38, 11, N'Khách sạn', 3000000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (39, 11, N'Vui chơi', 500000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (40, 12, N'Mua dép', 90000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (41, 12, N'Mua bánh mì', 50000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (42, 12, N'Mua nước', 30000.0000)
INSERT [dbo].[KhoanChiTieu] ([IDKhoanChi], [IDChuyenDi], [TenKhoanChi], [SoTien]) VALUES (43, 12, N'Xăng xe', 90000.0000)
SET IDENTITY_INSERT [dbo].[KhoanChiTieu] OFF
GO
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (1, 1, 300000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (1, 2, 400000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (1, 3, 300000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (1, 4, 350000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (2, 2, 500000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (2, 3, 450000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (2, 4, 600000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (2, 6, 800000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (3, 3, 750000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (3, 7, 500000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (3, 8, 650000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (3, 9, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (4, 4, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (4, 5, 500000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (4, 6, 700000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (4, 9, 1200000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (5, 5, 600000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (5, 6, 700000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (5, 8, 900000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (5, 9, 800000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (5, 10, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (9, 2, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (9, 7, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (9, 9, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (9, 10, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (10, 6, 700000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (10, 8, 750000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (10, 9, 800000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (10, 10, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (11, 2, 500000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (11, 6, 950000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (11, 7, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (11, 10, 1000000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (12, 1, 500000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (12, 2, 100000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (12, 3, 80000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (12, 11, 700000.0000)
INSERT [dbo].[KhoanThu] ([IDChuyenDi], [IDNguoiDongTien], [SoTien]) VALUES (12, 12, 1000000.0000)
GO
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (1, 1)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (1, 2)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (1, 3)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (1, 4)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (2, 2)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (2, 3)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (2, 4)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (2, 6)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (3, 3)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (3, 7)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (3, 8)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (3, 9)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (4, 4)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (4, 5)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (4, 6)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (4, 9)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (5, 5)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (5, 6)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (5, 8)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (5, 9)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (5, 10)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (9, 2)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (9, 7)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (9, 9)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (9, 10)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (10, 6)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (10, 8)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (10, 9)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (10, 10)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (11, 2)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (11, 6)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (11, 7)
INSERT [dbo].[ThamGiaChuyenDi] ([IDChuyenDi], [IDThanhVien]) VALUES (11, 10)
GO
SET IDENTITY_INSERT [dbo].[ThanhVien] ON 

INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (1, N'Phạm Minh Vương')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (2, N'Phạm Văn Thật')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (3, N'Lưu Trường Vũ')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (4, N'Nguyễn Xuân Thanh')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (5, N'Quách Mai Nhi')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (6, N'Nguyễn Nguyệt Hà')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (7, N'Đinh Xuân Lan')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (8, N'Úc Bảo Thái')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (9, N'Nguyễn Quốc Thông')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (10, N'Hà Anh Tài')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (11, N'Vũ Phan Nhật Tài')
INSERT [dbo].[ThanhVien] ([ID], [TenThanhVien]) VALUES (12, N'Trần Thống Nhất')
SET IDENTITY_INSERT [dbo].[ThanhVien] OFF
GO
ALTER TABLE [dbo].[ChuyenDi]  WITH CHECK ADD  CONSTRAINT [FK_ChuyenDi_ThanhVien] FOREIGN KEY([Leader])
REFERENCES [dbo].[ThanhVien] ([ID])
GO
ALTER TABLE [dbo].[ChuyenDi] CHECK CONSTRAINT [FK_ChuyenDi_ThanhVien]
GO
ALTER TABLE [dbo].[HinhAnhChuyenDi]  WITH CHECK ADD  CONSTRAINT [FK_HinhAnhChuyenDi_ChuyenDi] FOREIGN KEY([IDChuyenDi])
REFERENCES [dbo].[ChuyenDi] ([IDChuyenDi])
GO
ALTER TABLE [dbo].[HinhAnhChuyenDi] CHECK CONSTRAINT [FK_HinhAnhChuyenDi_ChuyenDi]
GO
ALTER TABLE [dbo].[KhoanChiTieu]  WITH CHECK ADD  CONSTRAINT [FK_KhoanChiTieu_ChuyenDi] FOREIGN KEY([IDChuyenDi])
REFERENCES [dbo].[ChuyenDi] ([IDChuyenDi])
GO
ALTER TABLE [dbo].[KhoanChiTieu] CHECK CONSTRAINT [FK_KhoanChiTieu_ChuyenDi]
GO
ALTER TABLE [dbo].[KhoanThu]  WITH CHECK ADD  CONSTRAINT [FK_KhoanThu_ChuyenDi] FOREIGN KEY([IDChuyenDi])
REFERENCES [dbo].[ChuyenDi] ([IDChuyenDi])
GO
ALTER TABLE [dbo].[KhoanThu] CHECK CONSTRAINT [FK_KhoanThu_ChuyenDi]
GO
ALTER TABLE [dbo].[KhoanThu]  WITH CHECK ADD  CONSTRAINT [FK_KhoanThu_ThanhVien] FOREIGN KEY([IDNguoiDongTien])
REFERENCES [dbo].[ThanhVien] ([ID])
GO
ALTER TABLE [dbo].[KhoanThu] CHECK CONSTRAINT [FK_KhoanThu_ThanhVien]
GO
ALTER TABLE [dbo].[ThamGiaChuyenDi]  WITH CHECK ADD  CONSTRAINT [FK_ThamGiaChuyenDi_ChuyenDi] FOREIGN KEY([IDChuyenDi])
REFERENCES [dbo].[ChuyenDi] ([IDChuyenDi])
GO
ALTER TABLE [dbo].[ThamGiaChuyenDi] CHECK CONSTRAINT [FK_ThamGiaChuyenDi_ChuyenDi]
GO
ALTER TABLE [dbo].[ThamGiaChuyenDi]  WITH CHECK ADD  CONSTRAINT [FK_ThamGiaChuyenDi_ThanhVien] FOREIGN KEY([IDThanhVien])
REFERENCES [dbo].[ThanhVien] ([ID])
GO
ALTER TABLE [dbo].[ThamGiaChuyenDi] CHECK CONSTRAINT [FK_ThamGiaChuyenDi_ThanhVien]
GO
