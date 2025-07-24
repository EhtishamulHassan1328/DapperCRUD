using Dapper;
using DapperTest.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace DapperTest.Repositories
{
    public class VideoGameRepository : IVideoGameRepository
    {
        private readonly IConfiguration _configuration;

        public VideoGameRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<VideoGame>> GetAllAsync()
        {
            using var connection = GetConnection();
            var videoGames = await connection.QueryAsync<VideoGame>(
                "SELECT Id, Title, Publisher, Developer, ReleaseDate FROM VideoGames");
            return videoGames.ToList();
        }

        public async Task<VideoGame> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            var videoGame = await connection.QueryFirstOrDefaultAsync<VideoGame>(
                "Select * from VideoGames Where Id=@id", new { Id = id });
            return videoGame;
        }

        public async Task CreateAsync(VideoGame g1)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync(
                "Insert into VideoGames(Title, Publisher, Developer, ReleaseDate) VALUES (@Title, @Publisher, @Developer, @ReleaseDate)", g1);


        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync(
                "Delete from VideoGames where Id=@id", new { Id = id });
        }

        public async Task UpdateAsync(VideoGame g1)
        {
            using var connection = GetConnection();
            await connection
                .ExecuteAsync("UPDATE VideoGames SET Title = @Title, Publisher = @Publisher, Developer = @Developer, ReleaseDate = @ReleaseDate WHERE Id = @Id", g1);

        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

        }
    }
}
