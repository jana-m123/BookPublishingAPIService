using BookPublishingService.Core.Models;


namespace BookPublishingService.Core.Interfaces
{
    public interface IBookPublishingRepository
    {
        List<BookAuthor> GetPublisherDetails();
        List<BookAuthor> GetAuthorDetails();
        List<BookAuthor> GetPublisherDetailsbySP();
        List<BookAuthor> GetAuthorDetailsbySP();
        Decimal TotalPriceofBooks();
        string SaveBookList(List<BookAuthor> BookList);
    }
}
