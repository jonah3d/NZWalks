using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;

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

        public async Task<List<Walk>> GetAllAsync()
        {
           return await dbContext.Walks.
                Include("Difficulty").
                Include("Region")
                .ToListAsync();

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
