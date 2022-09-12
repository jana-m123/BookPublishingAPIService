
USE NLXAdministration
GO
Drop table NLXAdministration.Common.Publisher
Drop table NLXAdministration.Common.Author
Drop table NLXAdministration.Common.Book





IF NOT EXISTS (SELECT 1
		       FROM Sys.objects
			   WHERE name = 'Book'
			   AND Type = 'U')
BEGIN
PRINT 'Creating Table Common.Book'
CREATE TABLE Common.Book
(	
	Book_id			 BIGINT IDENTITY(1,1)				NOT NULL,
	Book_name		 VARCHAR(150)							NOT NULL,
	Price decimal(18,2) NULL,
	
	
	
	

	CONSTRAINT PK_BookId PRIMARY KEY CLUSTERED (Book_id)
	

)
END
ELSE
	PRINT 'Table Common.Book Already exists'
GO
-----------------------------------------------------------------------------
USE NLXAdministration
GO

IF NOT EXISTS (SELECT 1
		       FROM Sys.objects
			   WHERE name = 'Publisher'
			   AND Type = 'U')
BEGIN
PRINT 'Creating Table Common.Publisher'
CREATE TABLE Common.Publisher
(	
	Publisher_id		BIGINT IDENTITY(1,1)			 				NOT	NULL,	
	Publisher_name		 VARCHAR(150)							NOT NULL,
	
	Book_id		BIGINT			 					NULL,		

	

	CONSTRAINT PK_Book PRIMARY KEY CLUSTERED (Publisher_id)	
	
	,CONSTRAINT FK_Publisher_Book FOREIGN KEY (Book_id)
    REFERENCES NLXAdministration.Common.Book (Book_id)

)
END
ELSE
	PRINT 'Table Common.Publisher Already exists'
GO
------------------------------------------------------------------------------------
-----------------------------------------------------------------------------
USE NLXAdministration
GO


IF NOT EXISTS (SELECT 1
		       FROM Sys.objects
			   WHERE name = 'Author'
			   AND Type = 'U')
BEGIN
PRINT 'Creating Table Common.Author'
CREATE TABLE Common.Author
(	
	Author_id			 BIGINT IDENTITY(1,1)				NOT NULL,
	Author_firstname		 VARCHAR(150)							NOT NULL,
	Author_lastname		 VARCHAR(150)							NOT NULL,
	Book_id		BIGINT			 					NULL,	
	
	

	CONSTRAINT PK_Author PRIMARY KEY CLUSTERED (Author_id)
	,CONSTRAINT FK_Author_Book FOREIGN KEY (Book_id)
    REFERENCES NLXAdministration.Common.Book (Book_id)

)
END
ELSE
	PRINT 'Table Common.Author Already exists'
GO

-----Sample Insert script---------------------------------

use NLXAdministration


INSERT INTO Common.Book (book_name,price) VALUES
('The World''s First Love: Mary  Mother of God',   1000.00),
('The Illuminati',   900.02),
('The Servant Leader',   456.98),
('What Life Was Like in the Jewel in the Crown: British India  AD 1600-1905',   675.00),
('The Life of PI',   1200.00)

INSERT INTO Common.Publisher (publisher_name,book_id) VALUES
('10/18',1),
('1st Book Library',2),
('1st World Library',3),
('A & C Black (Childrens books)',4),
('A Harvest Book/Harcourt Inc.',5)


INSERT INTO Common.Author (author_firstname,author_lastname,book_id) VALUES
('A. Bartlett','Giamatti',1),
('A. Elizabeth ','Delany' ,2),
('A. Merritt','Rose' ,3),
('A. Roger ','Merrill', 4),
('A. Walton ','Litz', 5)

-------------- 2 get sp created and 1 Insert sp created and 1 User -Defined Table type created ---------------------
USE [NLXAdministration]
GO

/****** Object:  StoredProcedure [common].[proc_GetPublisherDetailsbySP]    Script Date: 2/7/2022 3:22:01 PM ******/
IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = 'proc_GetPublisherDetailsbySP')
DROP PROCEDURE Common.[proc_GetPublisherDetailsbySP]
GO

/****** Object:  StoredProcedure [common].[proc_GetPublisherDetailsbySP]    Script Date: 7/20/2022 12:04:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:	Janaranjani S	
-- Create date: 09/12/2022
-- Description:	Get the publisher details of book 
-- History:
--  Version    Date		UserStory			Description

-- =============================================
CREATE PROCEDURE common.[proc_GetPublisherDetailsbySP]  
	
AS
BEGIN
   	
			select 
	publisher_name AS Publisher,
	author.author_lastname AS AuthorLastName,
	author.author_firstname AS AuthorFirstName,
	author.author_lastname+' , '+author.author_firstname AS AuthorName,
	book.book_name As Title

from NLXAdministration.common.Book book
inner join NLXAdministration.Common.Publisher publisher on publisher.book_id=book.book_id
inner join NLXAdministration.Common.Author author on author.book_id=book.book_id
order by publisher.publisher_name,AuthorName,Title

END


GO
--------------------------------------------------------------------------------
USE [NLXAdministration]
GO

/****** Object:  StoredProcedure [common].[GetAuthorDetailsbySP]    Script Date: 2/7/2022 3:22:01 PM ******/
IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = 'GetAuthorDetailsbySP')
DROP PROCEDURE Common.[GetAuthorDetailsbySP]
GO

/****** Object:  StoredProcedure [common].[GetAuthorDetailsbySP]    Script Date: 7/20/2022 12:04:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:	Janaranjani S	
-- Create date: 09/12/2022
-- Description:	Get the author details of book 
-- History:
--  Version    Date		UserStory			Description

-- =============================================
CREATE PROCEDURE common.[GetAuthorDetailsbySP]  
	
AS
BEGIN
   	
		select 	
		author.author_lastname AS AuthorLastName,
	author.author_firstname AS AuthorFirstName,
	author.author_lastname+' , '+author.author_firstname AS AuthorName,
	book.book_name As Title

from NLXAdministration.common.Book book
inner join NLXAdministration.Common.Author author on author.book_id=book.book_id
order by author.author_lastname,author.author_firstname,Title

END


GO

USE [NLXAdministration]
GO

IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = 'proc_InsertBookListDetails')
DROP PROCEDURE Common.[proc_InsertBookListDetails]
GO


/****** Object:  UserDefinedTableType [Common].[udt_BookDetails]    Script Date: 10/26/2021 11:32:07 AM ******/
IF EXISTS (SELECT * FROM sys.types WHERE is_table_type = 1 AND name = 'udt_BookDetails')
DROP TYPE Common.udt_BookDetails
GO

/****** Object:  UserDefinedTableType [Common].[udt_BookDetails]    Script Date: 10/26/2021 11:32:07 AM ******/
CREATE TYPE Common.udt_BookDetails AS TABLE
(
	Publisher VARCHAR(150) NULL,
	Title VARCHAR(100) NULL,	
	AuthorLastName VARCHAR(255) NULL,	  
	AuthorFirstName VARCHAR(100) NULL,
	Price decimal(18,2) NULL
	
)
GO

USE [NLXAdministration]
GO

/****** Object:  StoredProcedure [common].[proc_InsertBookListDetails]    Script Date: 2/4/2022 3:15:29 PM ******/
IF EXISTS(SELECT 1 FROM sys.procedures WHERE Name = 'proc_InsertBookListDetails')
DROP PROCEDURE common.[proc_InsertBookListDetails]
GO

/****** Object:  StoredProcedure [common].[proc_InsertBookListDetails]    Script Date: 2/4/2022 3:15:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--/****** Object:  StoredProcedure [common].[proc_InsertBookListDetails]    Script Date: 10/25/2021 11:46:29 AM ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

-- =============================================
-- Author:		Janaranjani S
-- Create date: 2022-9-12
-- Description:	Insert book details into repective tables 
-- History:

-- =============================================
CREATE   PROCEDURE common.[proc_InsertBookListDetails]
(
@BookItemTable common.udt_BookDetails READONLY,
@ReturnBookID VARCHAR(255) OUTPUT
)

AS
BEGIN
   
SET NOCOUNT ON;
			
	DECLARE @BOOKID BIGINT;
		
	BEGIN TRY	
	
	BEGIN
		
       
      		BEGIN TRAN [TRAN_INSERT_BOOK_DETAILS]        
           
				INSERT INTO common.Book
				   (
					Book_name
				   ,Price
				   			  
				   )
				SELECT 
		 			Tempbook.Title,
					Tempbook.Price	
				
				FROM  @BookItemTable Tempbook    				
					-- save the ID
				SET @BookID = SCOPE_IDENTITY()       


				INSERT INTO Common.Publisher
				   (
					publisher_name,
					book_id
				   )
				SELECT
					
					Tempbook.Publisher
					,bk.Book_id
			
				FROM  @BookItemTable Tempbook 
				INNER JOIN Common.Book bk ON 		bk.Book_name=Tempbook.Title
				 
				INSERT INTO Common.Author
						   (
							Author_firstname		,
							Author_lastname	,	
						    Book_id
						   ) 
				 SELECT 
							Tempbook.AuthorFirstName,
							Tempbook.AuthorLastName
							,bk.Book_id
			
				FROM  @BookItemTable Tempbook	
				INNER JOIN Common.Book bk ON 		bk.Book_name=Tempbook.Title			  
				
				

				COMMIT TRAN [TRAN_INSERT_BOOK_DETAILS]	
				
				SET @ReturnBookID = CAST(@BookID AS varchar)
				
				SELECT @ReturnBookID						
			   
	END

END TRY
BEGIN CATCH
        -- rollback entire copy transaction       
         
            ROLLBACK TRAN [TRAN_INSERT_BOOK_DETAILS]    
 
		  
       	
END CATCH

END




GO


--------------------------------------------------------------------------

--------------Select Query first 5 API methods---------- 
select 
	publisher_name AS Publisher,
	author.author_lastname AS AuthorLastName,
	author.author_firstname AS AuthorFirstName,
	author.author_lastname+' , '+author.author_firstname AS AuthorName,
	book.book_name As Title

from NLXAdministration.common.Book book
inner join NLXAdministration.Common.Publisher publisher on publisher.book_id=book.book_id
inner join NLXAdministration.Common.Author author on author.book_id=book.book_id
order by publisher.publisher_name,AuthorName,Title

exec common.[proc_GetPublisherDetailsbySP]  
--------
select 	
author.author_lastname AS AuthorLastName,
	author.author_firstname AS AuthorFirstName,
	author.author_lastname+' , '+author.author_firstname AS AuthorName,
	book.book_name As Title

from NLXAdministration.common.Book book
inner join NLXAdministration.Common.Author author on author.book_id=book.book_id
order by author.author_lastname,author.author_firstname,Title


exec common.[GetAuthorDetailsbySP]

-------------------------------------
select 	
SUM(TRY_CONVERT(decimal(18,2), Price))

from NLXAdministration.common.Book book


select * from NLXAdministration.common.Book 
select * from NLXAdministration.common.Publisher 
select * from NLXAdministration.common.Author 