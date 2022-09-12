
using BookPublishingService.Core.Interfaces;
using BookPublishingService.Core.Models;
using BookPublishingService.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;

namespace BookPublishingService.Infrastructure.Repository
{
    public class BookPublishingRepository : IBookPublishingRepository
    {
        private readonly IDatabaseService _databaseService;
        public BookPublishingRepository(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public List<BookAuthor> GetPublisherDetails()
        {
            try
            {
                var result = _databaseService.GetPublisherDetails().Result;

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public List<BookAuthor> GetAuthorDetails()
        {
            try
            {
                var result = _databaseService.GetAuthorDetails().Result;

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public List<BookAuthor> GetPublisherDetailsbySP()
        {
            try
            {
                var result = _databaseService.GetPublisherDetailsbySP().Result;

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public List<BookAuthor> GetAuthorDetailsbySP()
        {
            try
            {
                var result = _databaseService.GetAuthorDetailsbySP().Result;

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Decimal TotalPriceofBooks()
        {
            try
            {
                var result = _databaseService.TotalPriceofBooks().Result;

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public string SaveBookList(List<BookAuthor> BookList)
        {
            string returnBookID = "";
           
            try
            {

                


                var PacakgeItemDetailsDt = new DataTable();
                PacakgeItemDetailsDt.Columns.Add("Publisher", typeof(string));
                PacakgeItemDetailsDt.Columns.Add("Title", typeof(string));
                PacakgeItemDetailsDt.Columns.Add("AuthorLastName", typeof(string));
                PacakgeItemDetailsDt.Columns.Add("AuthorFirstName", typeof(string));
                PacakgeItemDetailsDt.Columns.Add("Price", typeof(decimal));

                foreach (var item in BookList)
                {
                    var pdrow = PacakgeItemDetailsDt.NewRow();
                    pdrow["Publisher"] = item.Publisher;
                    pdrow["Title"] = item.Title;
                    pdrow["AuthorLastName"] = item.AuthorLastName;
                    pdrow["AuthorFirstName"] = item.AuthorFirstName;
                    pdrow["Price"] = item.Price;
                    PacakgeItemDetailsDt.Rows.Add(pdrow);
                }
                

                var outputParam = new SqlParameter
                {
                    ParameterName = "ReturnPackageID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output,
                };

               
                
                //foreach (var con in Connectionstrings)
                // {
                //var DestinationId = DeploymentConfiguration.Where(x => x.SQLConnectionString.Contains(Connectionstrings)).Select(x => x.DeploymentDestinationName).First();
                returnBookID = _databaseService.SaveBookList(PacakgeItemDetailsDt, outputParam);
               
                return returnBookID;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message, ex.InnerException);
            }

        }


    }
}



