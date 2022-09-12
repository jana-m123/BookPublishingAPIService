using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPublishingService.Core.Models
{
    public class BookAuthor
    {
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }

       
    }
}
