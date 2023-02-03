using System.Collections.Generic;
using WebApplication5.Data.Models;

namespace WebApplication5.Models
{
    public class SearchResultModel
    {
        public List<Page> pages { set; get; }
        public string searchText { set; get; }
        public SearchResultModel() { 
            pages = new List<Page>();
        }
    }
}
