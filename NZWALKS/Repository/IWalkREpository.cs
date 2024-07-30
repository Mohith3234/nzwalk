using NZWALKS.Model.Domine;
namespace NZWALKS.Repository
{
    public interface IWalkREpository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAyncc(string? filterOn,string? filterQuery,string? soryBy=null,bool? isAscending=true,int pageNumber=1,int pageSize=1000);

        Task<Walk> GetByIdAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id,Walk walk);

        Task<Walk> DeleteAsync(Guid id);
    }
}
