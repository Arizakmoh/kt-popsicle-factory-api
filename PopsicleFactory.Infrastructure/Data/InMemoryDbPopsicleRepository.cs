using PopsicleFactory.Domain.Entities;
using PopsicleFactory.Domain.Interfaces;
using System.Collections.Concurrent;

namespace PopsicleFactory.Infrastructure.Data;

public class InMemoryDbPopsicleRepository : IPopsicleRepository
{
    private readonly ConcurrentDictionary<Guid, Popsicle> _popsicles = new();    
    public Task AddAsync(Popsicle popsicle)    {
        _popsicles[popsicle.Id] = popsicle;
        return Task.CompletedTask;
    }
    public Task<Popsicle?> GetByIdAsync(Guid id)    {
        _popsicles.TryGetValue(id, out var popsicle);
        return Task.FromResult(popsicle);
    }
    public Task<IEnumerable<Popsicle>> GetAllAsync()   {
        return Task.FromResult(_popsicles.Values.AsEnumerable());
    }
    public Task UpdateAsync(Popsicle popsicle)
    {
        _popsicles[popsicle.Id] = popsicle;
        return Task.CompletedTask;
    }
    public Task DeleteAsync(Guid id)
    {
        _popsicles.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}