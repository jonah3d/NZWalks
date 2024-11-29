namespace NZWalks_API.Repositories
{
    public interface IWalkRepository
    {
        Task<Models.Domain.Walk> CreateAsync(Models.Domain.Walk walk);
    }
}
