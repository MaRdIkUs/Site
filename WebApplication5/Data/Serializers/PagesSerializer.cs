using System;
using System.Collections.Generic;
using System.IO;
using WebApplication5.Data.Interfaces;
using WebApplication5.Data.Models;

namespace WebApplication5.Data.Serializers
{
    public class PagesSerializer : IPage
    {
        public bool addPage(Page page)
        {
            string fileName = generateFileName(page);
            File.WriteAllText(fileName,page.Content);
            return true;
        }

        public bool editPage(Page oldPage, Page newPage)
        {
            removePage(oldPage);
            addPage(newPage);
            return true;
        }

        public List<Page> GetAllPages()
        {
            List<Page> list = new List<Page>();
            foreach (string s in getFiles())
            {
                Page page = new Page();
                page.Id = Convert.ToInt32(s.Split("_")[0]);
                page.PageType = Convert.ToInt32(s.Split("_")[1]);
                page.Title = s.Split("_")[2];
                StreamReader reader = new StreamReader(Startup.newsDirectory + page.Id + "_" + page.PageType + "_" + page.Title + ".txt");
                page.Content = reader.ReadToEnd();
                page.shortDescrition = page.Content.Length > 50 ? page.Content.Substring(0, 50) : page.Content;
                reader.Close();
                list.Add(page);
            }
            return list;
        }

        public int GetMaxId()
        {
            int maxId = 0;
            foreach (string s in getFiles())
            {
                maxId = Convert.ToInt32(s.Split("_")[0]);
            }
            return maxId;
        }

        public Page GetPage(int id)
        {
            foreach (string s in getFiles())
            {
                if (Convert.ToInt32(s.Split("_")[0]) == id) {
                    Page page = new Page();
                    page.Id = Convert.ToInt32(s.Split("_")[0]);
                    page.PageType = Convert.ToInt32(s.Split("_")[1]);
                    page.Title = s.Split("_")[2];
                    StreamReader reader = new StreamReader(Startup.newsDirectory + page.Id + "_" + page.PageType + "_" + page.Title + ".txt");
                    page.Content = reader.ReadToEnd();
                    page.shortDescrition = page.Content.Length > 50 ? page.Content.Substring(0, 50) : page.Content;
                    reader.Close();
                    return page;
                }
            }
            return null;
        }

        public List<Page> GetPages(int PageType)
        {
            List<Page> pages = new List<Page>();
            foreach (string s in getFiles())
            {
                if (Convert.ToInt32(s.Split("_")[1]) == PageType)
                {
                    Page page = new Page();
                    page.Content = s.Split("_")[0];
                    page.Id = Convert.ToInt32(s.Split("_")[0]);
                    page.PageType = Convert.ToInt32(s.Split("_")[1]);
                    page.Title = s.Split("_")[2];
                    StreamReader reader = new StreamReader(Startup.newsDirectory + page.Id + "_" + page.PageType + "_" + page.Title + ".txt");
                    page.Content = reader.ReadToEnd();
                    page.shortDescrition = page.Content.Length > 50 ? page.Content.Substring(0, 50) : page.Content;
                    reader.Close();
                    pages.Add(page);
                }
            }
            return pages;
        }

        public bool removePage(Page page)
        {
            File.Delete(generateFileName(page));
            return true;
        }

        private string generateFileName(Page page)
        {
            return Startup.newsDirectory + page.Id + "_" + page.PageType + "_" + page.Title + ".txt";
        }
        private string[] getFiles()
        {
            FileInfo[] files = new DirectoryInfo(Startup.newsDirectory).GetFiles();
            string[] result = new string[files.Length];
            int i = 0;
            foreach (FileInfo info in files)
            {
                result[i++] = info.Name.Substring(0,info.Name.Length-4);
            }
            return result;
        }   
    }
}
