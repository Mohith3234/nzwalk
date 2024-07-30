using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWALKS.Data;
using NZWALKS.DTO;
using NZWALKS.Model.Domine;

namespace NZWALKS.Repository
{
    public class SQLWalkRepository : IWalkREpository
    {
        private readonly DbcontextClass dbcontextClass;

        public SQLWalkRepository(DbcontextClass dbcontextClass)
        {
            this.dbcontextClass = dbcontextClass;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbcontextClass.walks.AddAsync(walk);
            await dbcontextClass.SaveChangesAsync();
            //convert walk domine model to dto

            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var exitvalue= await dbcontextClass.walks.SingleOrDefaultAsync(x => x.Id == id);
            if (exitvalue == null)
            {
                return null;
            }
            dbcontextClass.walks.Remove(exitvalue);
            await dbcontextClass.SaveChangesAsync();
            return exitvalue;
        }

        public async Task<List<Walk>> GetAllAyncc(string? filterOn, string? filterQuery, string? soryBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walk=dbcontextClass.walks.Include("Deficulty").Include("Region").AsQueryable();
           //filterring
           if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walk=dbcontextClass.walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
           //sorting

            if(string.IsNullOrWhiteSpace(soryBy)==false)
            {
                if(soryBy.Equals("Name",StringComparison.OrdinalIgnoreCase) )
                {
                    if(isAscending == true)
                    {
                        walk = dbcontextClass.walks.OrderBy(x => x.Name);
                    }
                    else
                    {
                        walk = dbcontextClass.walks.OrderByDescending(x => x.Name);
                    }
                    
                }
                else if(soryBy.Equals("lengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending == true)
                    {
                        walk = dbcontextClass.walks.OrderBy(x => x.LengthInKm);
                    }
                    else
                    {
                        walk = dbcontextClass.walks.OrderByDescending(x => x.LengthInKm);
                    }

                }
            }
            var skipResults = (pageNumber - 1) * (pageSize);

           return await walk.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await dbcontextClass.walks.Include("Deficulty").Include("Region").ToListAsync();
        }
        //sorting
        

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbcontextClass.walks.Include("Deficulty").Include("Region").FirstOrDefaultAsync(walk => walk.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id,Walk walk)
        {

           var ExitValue=await dbcontextClass.walks.Include("Deficulty").Include("Region").SingleOrDefaultAsync(x=>x.Id==id);
            if(ExitValue == null) 
            {
                return null;
            }
            ExitValue.Name = walk.Name;
            ExitValue.Description = walk.Description;
            ExitValue.LengthInKm = walk.LengthInKm;
            ExitValue.WalkImageUrl = walk.WalkImageUrl;
            ExitValue.DeficultyId = walk.DeficultyId;
            ExitValue.RegionId  = walk.RegionId;
            await dbcontextClass.SaveChangesAsync();
            return ExitValue;
        }
       
    }
}
