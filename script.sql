USE [master]
GO
/****** Object:  Database [BooksL]    Script Date: 5/11/2018 8:23:43 AM ******/
CREATE DATABASE [BooksL]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BooksL', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\BooksL.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BooksL_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\BooksL_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BooksL] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BooksL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BooksL] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BooksL] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BooksL] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BooksL] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BooksL] SET ARITHABORT OFF 
GO
ALTER DATABASE [BooksL] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BooksL] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BooksL] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BooksL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BooksL] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BooksL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BooksL] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BooksL] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BooksL] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BooksL] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BooksL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BooksL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BooksL] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BooksL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BooksL] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BooksL] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BooksL] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BooksL] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BooksL] SET  MULTI_USER 
GO
ALTER DATABASE [BooksL] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BooksL] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BooksL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BooksL] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BooksL] SET DELAYED_DURABILITY = DISABLED 
GO
USE [BooksL]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 5/11/2018 8:23:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[AuthorName] [nvarchar](150) NOT NULL,
	[History] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Book]    Script Date: 5/11/2018 8:23:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[CateID] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
	[PubID] [int] NOT NULL,
	[Summary] [nvarchar](200) NOT NULL,
	[ImgUrl] [nvarchar](150) NOT NULL,
	[Price] [float] NOT NULL,
	[Quantity] [nvarchar](100) NOT NULL,
	[CreateDay] [date] NOT NULL,
	[ModifiedDay] [date] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/11/2018 8:23:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CateID] [int] IDENTITY(1,1) NOT NULL,
	[CateName] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comment]    Script Date: 5/11/2018 8:23:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[BookID] [int] NOT NULL,
	[CommentContent] [nvarchar](max) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 5/11/2018 8:23:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[PubID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[PubID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/11/2018 8:23:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserName] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Author] ([AuthorID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Category] FOREIGN KEY([CateID])
REFERENCES [dbo].[Category] ([CateID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Category]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Publisher] FOREIGN KEY([PubID])
REFERENCES [dbo].[Publisher] ([PubID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Publisher]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Book] FOREIGN KEY([BookID])
REFERENCES [dbo].[Book] ([BookID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Book]
GO
USE [master]
GO
ALTER DATABASE [BooksL] SET  READ_WRITE 
GO



--getCategory(): Observable<Category[]> {
--return this.http.get(this.CatesUrl).map((res: Response) => res.json());
--}
--getCateId(id: number): Observable<Category> {
--const url = `${this.CatesUrl}/${id}`;
--return this.http.get(url, this.CatesUrl).map((res: Response) => res.json());
--}
--createCategory(cate: Category): Observable<Category> {
--return this.http.post(this.CatesUrl, JSON.stringify(cate), { headers: this.headers })
--.map((res: Response) => res.json());
--}
--updateCategory (cate: Category): Observable<Category> {
--return this.http.put(`${this.CatesUrl}/${cate.CateID}`, JSON.stringify(cate), { headers: this.headers })
--.map((res: Response) => res.json());
--}
