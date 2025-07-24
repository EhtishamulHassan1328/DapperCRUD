using DapperTest.Models;

namespace DapperTest.Repositories
{
    public interface IVideoGameRepository
    {
        Task<List<VideoGame>> GetAllAsync();

        Task<VideoGame> GetByIdAsync(int id);

        Task CreateAsync(VideoGame game);

        Task DeleteAsync(int id);

        Task UpdateAsync(VideoGame v1);
        
    }
}
