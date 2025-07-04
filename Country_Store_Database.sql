USE [master]
GO
/****** Object:  Database [CountryDB]    Script Date: 02-07-2025 15:55:49 ******/
CREATE DATABASE [CountryDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Country', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\Country.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Country_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\Country_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CountryDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CountryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CountryDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CountryDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CountryDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CountryDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CountryDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CountryDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CountryDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CountryDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CountryDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CountryDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CountryDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CountryDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CountryDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CountryDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CountryDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CountryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CountryDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CountryDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CountryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CountryDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CountryDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CountryDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CountryDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CountryDB] SET  MULTI_USER 
GO
ALTER DATABASE [CountryDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CountryDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CountryDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CountryDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CountryDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CountryDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CountryDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [CountryDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CountryDB]
GO
/****** Object:  Table [dbo].[City]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](100) NULL,
	[PinCode] [nvarchar](10) NULL,
	[Population] [int] NULL,
	[StateId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](100) NULL,
	[CountryCode] [nvarchar](10) NULL,
	[Continent] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](100) NULL,
	[Capital] [nvarchar](100) NULL,
	[Language] [nvarchar](50) NULL,
	[CountryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[StoreId] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](100) NULL,
	[Description] [nvarchar](255) NULL,
	[OpeningTime] [time](7) NULL,
	[ClosingTime] [time](7) NULL,
	[EstablishedDate] [date] NULL,
	[ContactNumber] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
	[CountryId] [int] NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[CountryAccess] [bit] NOT NULL,
	[StateAccess] [bit] NOT NULL,
	[CityAccess] [bit] NOT NULL,
	[StoreAccess] [bit] NULL,
	[UserAccess] [bit] NULL,
	[AdminAccess] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Role] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserPermission] ADD  CONSTRAINT [DF_CountryAccess]  DEFAULT ((0)) FOR [CountryAccess]
GO
ALTER TABLE [dbo].[UserPermission] ADD  CONSTRAINT [DF_StateAccess]  DEFAULT ((0)) FOR [StateAccess]
GO
ALTER TABLE [dbo].[UserPermission] ADD  CONSTRAINT [DF_CityAccess]  DEFAULT ((0)) FOR [CityAccess]
GO
ALTER TABLE [dbo].[UserPermission] ADD  CONSTRAINT [DF_UserPermission_StoreAccess]  DEFAULT ((0)) FOR [StoreAccess]
GO
ALTER TABLE [dbo].[UserPermission] ADD  CONSTRAINT [DF_UserPermission_UserAccess]  DEFAULT ((0)) FOR [UserAccess]
GO
ALTER TABLE [dbo].[UserPermission] ADD  CONSTRAINT [DF_UserPermission_AdminAccess]  DEFAULT ((0)) FOR [AdminAccess]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([CityId])
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([StateId])
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
    SELECT 
        UserId,
        Username,
        Password,
        FullName,
        Role,
        IsActive
    FROM 
        Users
    ORDER BY 
        FullName
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @FullName NVARCHAR(100)=Null,
    @Role NVARCHAR(50)=null,
	@IsActive BIT = 1
AS
BEGIN
    INSERT INTO Users (Username, Password, FullName, Role, IsActive)
    VALUES (@Username, @Password, @FullName, @Role, 1);
END;
GO
/****** Object:  StoredProcedure [dbo].[PL_CityGetByStateId]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PL_CityGetByStateId]
    @StateId INT
AS
BEGIN
    SELECT CityId, CityName FROM City WHERE StateId = @StateId
END
GO
/****** Object:  StoredProcedure [dbo].[PL_CountryGetAll]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PL_CountryGetAll]
AS
BEGIN
    SELECT CountryId, CountryName FROM Country
END
GO
/****** Object:  StoredProcedure [dbo].[PL_StateGetByCountryId]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PL_StateGetByCountryId]
    @CountryId INT
AS
BEGIN
    SELECT StateId, StateName FROM State WHERE CountryId = @CountryId
END
GO
/****** Object:  StoredProcedure [dbo].[PL_StoreAddOrUpdate]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PL_StoreAddOrUpdate]
    @StoreId INT,
    @StoreName NVARCHAR(100),
    @OpeningTime TIME,
    @ClosingTime TIME,
    @CountryId INT,
    @StateId INT,
    @CityId INT
AS
BEGIN
    IF @StoreId = 0
    BEGIN
        INSERT INTO Store (
            StoreName, OpeningTime, ClosingTime,
            CountryId, StateId, CityId
        )
        VALUES (
            @StoreName, @OpeningTime, @ClosingTime,
            @CountryId, @StateId, @CityId
        )
    END
    ELSE
    BEGIN
        UPDATE Store
        SET StoreName = @StoreName,
            OpeningTime = @OpeningTime,
            ClosingTime = @ClosingTime,
            CountryId = @CountryId,
            StateId = @StateId,
            CityId = @CityId
        WHERE StoreId = @StoreId
    END
END

GO
/****** Object:  StoredProcedure [dbo].[PL_StoreDelete]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PL_StoreDelete]
    @StoreId INT
AS
BEGIN
    DELETE FROM Store WHERE StoreId = @StoreId
END
GO
/****** Object:  StoredProcedure [dbo].[PL_StoreGetAll]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PL_StoreGetAll]
AS
BEGIN
    SELECT 
        s.StoreId,
        s.StoreName,
        s.Description,
        s.OpeningTime,
        s.ClosingTime,
        s.EstablishedDate,
        s.ContactNumber,
        s.Email,
        c.CountryName,
        st.StateName,
        ci.CityName
    FROM Store s
    INNER JOIN Country c ON s.CountryId = c.CountryId
    INNER JOIN State st ON s.StateId = st.StateId
    INNER JOIN City ci ON s.CityId = ci.CityId
END
GO
/****** Object:  StoredProcedure [dbo].[PL_StoreGetById]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PL_StoreGetById]
    @StoreId INT
AS
BEGIN
    SELECT 
        StoreId,
        StoreName,
        Description,
        OpeningTime,
        ClosingTime,
        EstablishedDate,
        ContactNumber,
        Email,
        CountryId,
        StateId,
        CityId
    FROM Store
    WHERE StoreId = @StoreId
END
GO
/****** Object:  StoredProcedure [dbo].[SaveUserPermissions]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveUserPermissions]
    @UserId INT,
    @AdminAccess BIT,
    @CountryAccess BIT,
    @StateAccess BIT,
    @CityAccess BIT,
    @StoreAccess BIT,
    @UserAccess BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete existing permission for the user
    DELETE FROM UserPermission WHERE UserId = @UserId;

    -- Insert new permission
    INSERT INTO UserPermission (
        UserId,
        AdminAccess,
        CountryAccess,
        StateAccess,
        CityAccess,
        StoreAccess,
        UserAccess
    )
    VALUES (
        @UserId,
        @AdminAccess,
        @CountryAccess,
        @StateAccess,
        @CityAccess,
        @StoreAccess,
        @UserAccess
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AddOrEditUser]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddOrEditUser]
    @UserId INT OUTPUT,
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @FullName NVARCHAR(100),
    @Role NVARCHAR(50),
    @IsActive BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Users WHERE UserId = @UserId)
    BEGIN
        UPDATE Users SET
            Username = @Username,
            Password = @Password,
            FullName = @FullName,
            Role = @Role,
            IsActive = @IsActive
        WHERE UserId = @UserId
    END
    ELSE
    BEGIN
        INSERT INTO Users (Username, Password, FullName, Role, IsActive)
        VALUES (@Username, @Password, @FullName, @Role, @IsActive)

        SET @UserId = SCOPE_IDENTITY() -- Get the new ID
    END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddOrUpdateCity]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddOrUpdateCity]
    @CityId INT = 0,
    @CityName NVARCHAR(100),
    @PinCode NVARCHAR(10),
    @Population INT,
    @StateId INT
AS
BEGIN
    IF @CityId = 0
    BEGIN
        INSERT INTO City (CityName, PinCode, Population, StateId)
        VALUES (@CityName, @PinCode, @Population, @StateId);
    END
    ELSE
    BEGIN
        UPDATE City
        SET CityName = @CityName,
            PinCode = @PinCode,
            Population = @Population,
            StateId = @StateId
        WHERE CityId = @CityId;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddOrUpdateCountry]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddOrUpdateCountry]
    @CountryId INT = 0,
    @CountryName NVARCHAR(100),
    @CountryCode NVARCHAR(10),
    @Continent NVARCHAR(50)
AS
BEGIN
    IF @CountryId = 0
    BEGIN
        INSERT INTO Country (CountryName, CountryCode, Continent)
        VALUES (@CountryName, @CountryCode, @Continent);
    END
    ELSE
    BEGIN
        UPDATE Country
        SET CountryName = @CountryName,
            CountryCode = @CountryCode,
            Continent = @Continent
        WHERE CountryId = @CountryId;
    END
END

GO
/****** Object:  StoredProcedure [dbo].[SP_AddOrUpdateState]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddOrUpdateState]
    @StateId INT = 0,
    @StateName NVARCHAR(100),
    @Capital NVARCHAR(100),
    @Language NVARCHAR(50),
    @CountryId INT
AS
BEGIN
    IF @StateId = 0
    BEGIN
        INSERT INTO State (StateName, Capital, Language, CountryId)
        VALUES (@StateName, @Capital, @Language, @CountryId);
    END
    ELSE
    BEGIN
        UPDATE State
        SET StateName = @StateName,
            Capital = @Capital,
            Language = @Language,
            CountryId = @CountryId
        WHERE StateId = @StateId;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_City_Delete]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_City_Delete]
    @CityId INT
AS
BEGIN
    DELETE FROM City WHERE CityId = @CityId;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_City_GetAll]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_City_GetAll]
AS
BEGIN
    SELECT C.*, S.StateName
    FROM City C
    JOIN State S ON C.StateId = S.StateId;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_City_GetById]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_City_GetById]
    @CityId INT
AS
BEGIN
    SELECT 
        c.CityId,
        c.CityName,
        c.PinCode,
        c.Population,
        c.StateId,
		s.CountryId,
        s.StateName
    FROM City c
    INNER JOIN State s ON c.StateId = s.StateId
    WHERE c.CityId = @CityId;
END
EXEC SP_City_GetById @CityId = 2;
GO
/****** Object:  StoredProcedure [dbo].[SP_City_GetByStateId]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_City_GetByStateId]
    @StateId INT
AS
BEGIN
    SELECT CityId, CityName FROM City WHERE StateId = @StateId;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Country_Delete]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Country_Delete]
    @CountryId INT
AS
BEGIN
    BEGIN TRY
        -- First delete related states
        DELETE FROM State WHERE CountryId = @CountryId;

        -- Then delete the country
        DELETE FROM Country WHERE CountryId = @CountryId;
    END TRY
    BEGIN CATCH
        -- Handle error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Country_GetAll]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Country_GetAll]
AS
BEGIN
    SELECT * FROM Country;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Country_GetById]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Country_GetById]
    @CountryId INT
AS
BEGIN
    SELECT * FROM Country WHERE CountryId = @CountryId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteUser]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Delete
CREATE PROCEDURE [dbo].[sp_DeleteUser]
    @UserId INT
AS
BEGIN
    DELETE FROM Users WHERE UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserByID]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserByID]
    @UserId INT
AS
BEGIN
    SELECT * FROM Users WHERE UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserPermission]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_GetUserPermission]
    @UserId INT
AS
BEGIN
    SELECT 
        CountryAccess,
        StateAccess,
        CityAccess,
        StoreAccess,
        UserAccess,
        AdminAccess
    FROM UserPermission
    WHERE UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_LoginUser]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_LoginUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT UserId, Username, FullName, Role
    FROM Users
    WHERE Username = @Username
      AND Password = @Password
      AND IsActive = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_State_Delete]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_State_Delete]
    @StateId INT
AS
BEGIN
    DELETE FROM State WHERE StateId = @StateId;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_State_GetAll]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_State_GetAll]
AS
BEGIN
    SELECT S.*, C.CountryName
    FROM State S
    JOIN Country C ON S.CountryId = C.CountryId;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_State_GetByCountryId]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_State_GetByCountryId]
    @CountryId INT
AS
BEGIN
    SELECT StateId, StateName FROM State WHERE CountryId = @CountryId;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_State_GetById]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_State_GetById]
    @StateId INT
AS
BEGIN
    SELECT 
        s.StateId,
        s.StateName,
        s.Capital,
        s.Language,
        s.CountryId,
        c.CountryName -- ✅ Include CountryName
    FROM State s
    INNER JOIN Country c ON s.CountryId = c.CountryId
    WHERE s.StateId = @StateId
END

GO
/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 02-07-2025 15:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT TOP 1 
        UserId,
        Username,
        Password,
        FullName,
        Role,
        IsActive
    FROM Users
    WHERE Username = @Username AND Password = @Password AND IsActive = 1
END
GO
USE [master]
GO
ALTER DATABASE [CountryDB] SET  READ_WRITE 
GO
