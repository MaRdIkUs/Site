namespace WebApplication5.Data.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PageType { get; set; }
        public string shortDescrition { get; set; }

        public Page(int id, string title, string content, int pageType)
        {
            Id = id;
            Title = title;
            Content = content;
            PageType = pageType;
        }
        public Page() { }
    }
}
