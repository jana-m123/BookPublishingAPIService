using BookPublishingService.Core.Models;
using BookPublishingService.Infrastructure.Databases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookPublishingService.Infrastructure.Services
{
    public interface IDatabaseService
    {
        Task<List<BookAuthor>> GetPublisherDetails();
        Task<List<BookAuthor>> GetAuthorDetails();
        Task<List<BookAuthor>> GetPublisherDetailsbySP();
        Task<List<BookAuthor>> GetAuthorDetailsbySP();
        Task<decimal> TotalPriceofBooks();
        string SaveBookList(DataTable BookItemDetailsDt, SqlParameter outputParam);



    }

    public class DatabaseService : IDatabaseService
    {
        private IDatabase _database;
        public DatabaseService(IDatabase database)
        {
            _database = database;
        }

        public Task<List<BookAuthor>> GetPublisherDetails()
        {
            try
            {

                string sqlquery = @"select 
	publisher_name AS Publisher,author.author_lastname AS AuthorLastName,
	author.author_firstname AS AuthorFirstName,
	author.author_lastname+' , '+author.author_firstname AS AuthorName,
	book.book_name As Title

from NLXAdministration.common.Book book
inner join NLXAdministration.Common.Publisher publisher on publisher.book_id=book.book_id
inner join NLXAdministration.Common.Author author on author.book_id=book.book_id
order by publisher.publisher_name,AuthorName,Title";
                return _database.Query<BookAuthor>(sqlquery, null, false, 30);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }

        }
        public Task<List<BookAuthor>> GetAuthorDetails()
        {
            try
            {

                string sqlquery = @"select  author.author_lastname AS AuthorLastName,
	author.author_firstname AS AuthorFirstName,	
	author.author_lastname+' , '+author.author_firstname AS AuthorName,
	book.book_name As Title

from NLXAdministration.common.Book book
inner join NLXAdministration.Common.Author author on author.book_id=book.book_id
order by author.author_lastname,author.author_firstname,Title";
                return _database.Query<BookAuthor>(sqlquery, null, false, 30);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }

        }

        public Task<List<BookAuthor>> GetPublisherDetailsbySP()
        {
            try
            {

                string sqlquery = @"[NLXAdministration].common.[proc_GetPublisherDetailsbySP]";
                return _database.Query<BookAuthor>(sqlquery, null, true, 30);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }

        }
        public Task<List<BookAuthor>> GetAuthorDetailsbySP()
        {
            try
            {               

                string sqlquery = @"[NLXAdministration].common.[GetAuthorDetailsbySP]";
                return _database.Query<BookAuthor>(sqlquery, null, true, 30);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }

        }

        public Task<decimal> TotalPriceofBooks()
        {
            try
            {

                string sqlquery = @"select 	SUM(TRY_CONVERT(decimal(18,2), Price))
                                            from NLXAdministration.common.Book book";
                return _database.QueryValue<decimal>(sqlquery, null, false, 30);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }

        }

        public string SaveBookList( DataTable BookItemDetailsDt,  SqlParameter outputParam)
        {
            try
            {

                var sqlparams = new
                {
                    @BookItemTable = BookItemDetailsDt,
                  
                    @ReturnPackageID = outputParam

                };

                string returnBookID = "";

                using (SqlConnection sqlConnection = new SqlConnection("Server=PAC4NLXXDDW10V\\SQL16DED1;Database=NLXAdministration;Trusted_Connection=True;"))
                {
                    using (SqlCommand cmd = new SqlCommand("[Common].[proc_InsertBookListDetails]", sqlConnection))
                    {

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlConnection.Open();
                    

                        var param2 = cmd.Parameters.AddWithValue("@BookItemTable", BookItemDetailsDt);
                        param2.SqlDbType = SqlDbType.Structured;
                        param2.TypeName = "[Common].[udt_BookDetails]";
                       

                        cmd.Parameters.Add("@ReturnBookID", SqlDbType.VarChar, 4000);
                        cmd.Parameters["@ReturnBookID"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        returnBookID = (Convert.ToString(cmd.Parameters["@ReturnBookID"].Value));
                    }
                }

                return returnBookID;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }


        }


    }
}
