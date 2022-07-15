using Dapper;
using DapperLibrary.Models;
using DapperLibrary.Services.Interfaces;
using System.Data.SqlClient;

namespace DapperLibrary.Services
{
    public class BookService : IBookService
    {
        public async Task AddBookAsync(Book book)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperLibraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string insertSql = "INSERT INTO dbo.[Book] ([Title], [Author], [Description], [Price]) " + $"Values (@Title, @Author, @Description, @Price)";

            var Title = book.Title;

            var Author = book.Author;

            var Description = book.Description;

            var Price = book.Price;


            using (var connection = new SqlConnection(connectionString))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Title", Title);
                dynamicParameters.Add("Author", Author);
                dynamicParameters.Add("Description", Description);
                dynamicParameters.Add("Price", Price);

                await connection.ExecuteAsync(insertSql, dynamicParameters);
            }
        }

        public async Task<IEnumerable<dynamic>> GetAllBooksAsync()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperLibraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var querySql = "SELECT Id, Title, Author, Description, Price FROM dbo.[Book]";

            using (var connection = new SqlConnection(connectionString))
            {
                var users = await connection.QueryAsync<dynamic>(querySql);

                return users;
            }
        }
        public async Task<dynamic> GetSingleBookAsync(long id)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperLibraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var querySql = "SELECT * FROM dbo.[Book] " + $"WHERE Id=@Id";

            var Id = id;

            using (var connection = new SqlConnection(connectionString))
            {

                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", Id);

                var user = await connection.QueryAsync(querySql, dynamicParameters);

                return user;
            }
        }
        public async Task<bool> ModifyBookAsync(int id, Book book)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperLibraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            string updateSql = "UPDATE dbo.[Book] " + $"SET Title= @Title, Author=@Author, Description= @Description, Price= @Price " + $"WHERE Id=@Id ";

            var Id = id;
            var Title = book.Title;
            var Author = book.Author;
            var Description = book.Description;
            var Price = book.Price;

            using (var connection = new SqlConnection(connectionString))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", Id);
                dynamicParameters.Add("Title", Title);
                dynamicParameters.Add("Author", Author);
                dynamicParameters.Add("Description", Description);
                dynamicParameters.Add("Price", Price);

                try
                {
                    var result = await connection.ExecuteAsync(updateSql, dynamicParameters);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }
        public async Task<bool> DeleteBookAsync(int id)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DapperLibraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var deleteSql = "DELETE FROM dbo.[Book] " + $"WHERE Id=@Id";

            var Id = id;

            using (var connection = new SqlConnection(connectionString))
            {
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("Id", Id);

                try
                {
                    await connection.ExecuteAsync(deleteSql, dynamicParameters);
                }
                catch
                {
                    return false;
                }            
            }

            return true;
        }
    }
}
