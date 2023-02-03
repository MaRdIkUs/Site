using System.Collections.Generic;
using WebApplication5.Data.Models;

namespace WebApplication5.Models
{
    public class DefaultTitlePageModel
    {
        public bool access { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Page> Pages { get; set; }
    }
}
