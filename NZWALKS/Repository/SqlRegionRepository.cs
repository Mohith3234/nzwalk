using Microsoft.EntityFrameworkCore;
using NZWALKS.Data;
using NZWALKS.Model.Domine;

namespace NZWALKS.Repository
{
    public class SqlRegionRepository : RepositoryPattern
    {
        private readonly DbcontextClass dbcontextClass;
        public SqlRegionRepository(DbcontextClass dbcontextClass) 
        { 
            this.dbcontextClass = dbcontextClass;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbcontextClass.regions.AddRangeAsync(region);
            await dbcontextClass.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var existvalue=await dbcontextClass.regions.FirstOrDefaultAsync(s => s.Id == id);
            if (existvalue != null)
            {
                return null;
            }
          dbcontextClass.regions.Remove(existvalue);
            await dbcontextClass.SaveChangesAsync();
            return existvalue;
        }

        public async Task<List<Region>> GetAllAsync()
        {
           return await dbcontextClass.regions.ToListAsync();
           
        }

        public async Task<Region> GetByAsync(Guid id)
        {
            return await dbcontextClass.regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var exitingregion = await dbcontextClass.regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (exitingregion == null)
            {
                return null;
            }
            exitingregion.Id=region.Id;
            exitingregion.Name=region.Name;
            exitingregion.RegionUrl=region.RegionUrl;
            await dbcontextClass.SaveChangesAsync();
            return exitingregion;
        }
    }
}
