﻿using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IWalkRepository
    {
        Task<Models.Domain.Walk> CreateAsync(Models.Domain.Walk walk);
        Task<List<Walk>>GetAllAsync(string? filterOn = null,string?filterQuery = null,
            string? sortBy = null, bool isAscending = true,int pageNumber = 1, int pageSize = 1000
            );
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
