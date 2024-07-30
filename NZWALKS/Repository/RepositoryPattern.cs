using NZWALKS.Model.Domine;

namespace NZWALKS.Repository
{
    public interface RepositoryPattern
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id,Region region);

        Task<Region?> DeleteAsync(Guid id);
    }
   
}
