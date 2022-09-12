using Microsoft.AspNetCore.Mvc;
using BookPublishingService.Core.Interfaces;
using BookPublishingService.Core.Models;

namespace BookPublishingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class BookPublishingController : ControllerBase
    {
        private IBookPublishingRepository _publishingRepository;

        public BookPublishingController(IBookPublishingRepository publishingRepository)
        {
            _publishingRepository = publishingRepository;
        }       
       
        [HttpGet]
        [Route("GetPublisherDetails")]
        public ActionResult<List<BookAuthor>> GetPublisherDetails()
        {
            try
            {
                return _publishingRepository.GetPublisherDetails();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }


        }
        [HttpGet]
        [Route("GetAuthorDetails")]
        public ActionResult<List<BookAuthor>> GetAuthorDetails()
        {
            try
            {
                return _publishingRepository.GetAuthorDetails();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }


        }
        [HttpGet]
        [Route("GetPublisherDetailsbySP")]
        public ActionResult<List<BookAuthor>> GetPublisherDetailsbySP()
        {
            try
            {
                return _publishingRepository.GetPublisherDetailsbySP();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }


        }

        [HttpGet]
        [Route("GetAuthorDetailsbySP")]
        public ActionResult<List<BookAuthor>> GetAuthorDetailsbySP()
        {
            try
            {
                return _publishingRepository.GetAuthorDetailsbySP();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }


        }

        [HttpGet]
        [Route("TotalPriceofBooks")]
        public ActionResult<Decimal> TotalPriceofBooks()
        {
            try
            {
                return _publishingRepository.TotalPriceofBooks();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }


        }

        [HttpPost]
        [Route("SaveLargeBookList")]
        public ActionResult<string> SaveBookList(List<BookAuthor> booklist)
        {
            string Result;
            try
            {
                {
                    Result = _publishingRepository.SaveBookList(booklist);
                }
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpGet]
        [Route("healthcheck")]
        public string HealthCheck()
        {
            return "success";
        }
    }
}
