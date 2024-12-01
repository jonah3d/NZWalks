using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;
using System.Linq;

namespace NZWalks_API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
           await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingwalk =  await dbContext.Walks
                                .FirstOrDefaultAsync(w => w.Id == id);
             
             if(existingwalk == null)
            {
                return null;
            }

             dbContext.Walks.Remove(existingwalk);
            await dbContext.SaveChangesAsync();
            return existingwalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
             string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000
            )
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {

                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if (sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Length",StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x=> x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResult = (pageNumber - 1) * pageSize;



            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();

            /*  return await dbContext.Walks.
                   Include("Difficulty").
                   Include("Region")
                   .ToListAsync();*/

        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
         return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if(existingwalk == null)
            {
                return null;
            }

            existingwalk.Name = walk.Name;
            existingwalk.Description = walk.Description;
            existingwalk.LengthInKm = walk.LengthInKm;
            existingwalk.WalkImgUrl = walk.WalkImgUrl;
            existingwalk.DifficultyId = walk.DifficultyId;
            existingwalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return existingwalk;
        }
    }
}
