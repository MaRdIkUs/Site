using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using WebApplication5.Data.Interfaces;
using WebApplication5.Data.Models;

namespace WebApplication5.Data.Repositories
{
    public class PagesRepository : IPage
    {
        public bool addPage(Page page)
        {
            if (page == null || page.Title == null || page.Content == null)
                return false;
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_AddPage", connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // добавляем параметр
                SqlParameter IdParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = page.Id,
                };
                // добавляем параметр
                command.Parameters.Add(IdParam);
                SqlParameter TitleParam = new SqlParameter
                {
                    ParameterName = "@Title",
                    Value = page.Title,
                };
                // добавляем параметр
                command.Parameters.Add(TitleParam);
                SqlParameter ContentParam = new SqlParameter
                {
                    ParameterName = "@Content",
                    Value = page.Content,
                };
                // добавляем параметр
                command.Parameters.Add(ContentParam);
                SqlParameter PageTypeParam = new SqlParameter
                {
                    ParameterName = "@PageType",
                    Value = page.PageType,
                };
                // добавляем параметр
                command.Parameters.Add(PageTypeParam);
                var result = command.ExecuteScalar();
                return true;
            }
        }

        public string generateShortDescription(string content) {
            if (content == null)
                return null;
            if (content.Length < 50)
                return content;
            else
                return content.Substring(0, 50);
        }

        public Page GetPage(int id) {
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_GetPage", connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter IdParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id,
                };
                command.Parameters.Add(IdParam);
                var reader = command.ExecuteReader();
                Page page = new Page();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        page.Id = reader.GetInt32(0);
                        page.Title = reader.GetString(1);
                        page.Content = reader.GetString(2);
                        page.PageType = reader.GetInt32(3);
                        page.shortDescrition = generateShortDescription(page.Content);
                    }
                }
                reader.Close();
                return page;
            }
        }

        public List<Page> GetPages(int PageType)
        {
            List<Page> pages = new List<Page>();
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_GetPages", connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter PageTypeParam = new SqlParameter
                {
                    ParameterName = "@PageType",
                    Value = PageType,
                };
                command.Parameters.Add(PageTypeParam);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Page page = new Page();
                        page.Id = reader.GetInt32(0);
                        page.Title = reader.GetString(1);
                        page.Content = reader.GetString(2);
                        page.PageType = reader.GetInt32(3);
                        page.shortDescrition = generateShortDescription(page.Content);
                        pages.Add(page);
                    }
                }
                reader.Close();
            }
            return pages;
        }

        public bool removePage(Page page)
        {
            if (page == null || page.Title == null || page.Content == null)
                return false;
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_DeletePage", connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter IdParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = page.Id,
                };
                command.Parameters.Add(IdParam);
                var result = command.ExecuteNonQuery();
                return true;
            }
        }

        public bool editPage(Page oldPage, Page newPage) {
            if (newPage == null || newPage.Title == null || newPage.Content == null)
                return false;
            if (oldPage == null || oldPage.Title == null || oldPage.Content == null)
                return false;
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_EditPage", connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // добавляем параметр
                SqlParameter IdParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = oldPage.Id,
                };
                // добавляем параметр
                command.Parameters.Add(IdParam);
                SqlParameter TitleParam = new SqlParameter
                {
                    ParameterName = "@Title",
                    Value = newPage.Title,
                };
                // добавляем параметр
                command.Parameters.Add(TitleParam);
                SqlParameter ContentParam = new SqlParameter
                {
                    ParameterName = "@Content",
                    Value = newPage.Content,
                };
                // добавляем параметр
                command.Parameters.Add(ContentParam);
                SqlParameter PageTypeParam = new SqlParameter
                {
                    ParameterName = "@PageType",
                    Value = newPage.PageType,
                };
                // добавляем параметр
                command.Parameters.Add(PageTypeParam);
                var result = command.ExecuteScalar();
                return true;
            }
        }

        public List<Page> GetAllPages()
        {
            List<Page> pages = new List<Page>();
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_GetAllPages", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        Page page = new Page();
                        page.Id = reader.GetInt32(0);
                        page.Title = reader.GetString(1);
                        page.Content = reader.GetString(2);
                        page.PageType = reader.GetInt32(3);
                        page.shortDescrition = generateShortDescription(page.Content);
                        pages.Add(page);
                    }
                }
                reader.Close();
            }
            return pages;
        }

        public int GetMaxId() {
            using (SqlConnection connection = new SqlConnection(Startup.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_GetMaxId", connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                int id = 0;
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
                reader.Close();
                return id;
            }
        }
    }
}
