using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IRegionRepository
    {

        //will return a list of regions from the database
      Task<List<Region>> GetAllAsync();

       Task<Region?> GetByIdAsync(Guid id);

       Task <Region> CreateAsync(Region region);

       Task<Region?> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);
    }
}
