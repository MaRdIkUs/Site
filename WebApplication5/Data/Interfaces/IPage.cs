using System.Collections.Generic;
using WebApplication5.Data.Models;

namespace WebApplication5.Data.Interfaces
{
    public interface IPage
    {
        public bool addPage(Page page);
        public List<Page> GetPages(int PageType);
        public Page GetPage(int id);
        public bool removePage(Page page);
        public bool editPage(Page newPage, Page oldPage);
        public List<Page> GetAllPages();
        public int GetMaxId();
    }
}
