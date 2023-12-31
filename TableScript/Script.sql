USE [AirlineSystem]
GO
/****** Object:  Table [dbo].[flights]    Script Date: 28/09/2023 13:34:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flights](
	[flight_id] [bigint] NOT NULL,
	[route_id] [bigint] NULL,
	[departure_time] [datetime] NULL,
	[arrival_time] [datetime] NULL,
	[airline_id] [bigint] NULL,
 CONSTRAINT [PK_flights] PRIMARY KEY CLUSTERED 
(
	[flight_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[routes]    Script Date: 28/09/2023 13:34:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[routes](
	[route_id] [bigint] NOT NULL,
	[origin_city_id] [bigint] NULL,
	[destination_city_id] [bigint] NULL,
	[departure_date] [date] NULL,
 CONSTRAINT [PK_routes] PRIMARY KEY CLUSTERED 
(
	[route_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subscriptions]    Script Date: 28/09/2023 13:34:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscriptions](
	[agency_id] [bigint] NULL,
	[origin_city_id] [bigint] NULL,
	[destination_city_id] [bigint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[flights]  WITH CHECK ADD  CONSTRAINT [FK_flights_routes] FOREIGN KEY([route_id])
REFERENCES [dbo].[routes] ([route_id])
GO
ALTER TABLE [dbo].[flights] CHECK CONSTRAINT [FK_flights_routes]
GO
CREATE NONCLUSTERED INDEX [index_route_departure] ON [dbo].[flights]
(
	[route_id] ASC,
	[departure_time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO