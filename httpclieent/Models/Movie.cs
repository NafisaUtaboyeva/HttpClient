using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpclieent.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public long AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
