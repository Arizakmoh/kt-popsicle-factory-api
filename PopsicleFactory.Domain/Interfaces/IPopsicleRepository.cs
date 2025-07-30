using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PopsicleFactory.Domain.Entities;

namespace PopsicleFactory.Domain.Interfaces
{
    public interface IPopsicleRepository
    {
        Task<Popsicle?> GetByIdAsync(Guid id);
        Task<IEnumerable<Popsicle>> GetAllAsync();
        Task AddAsync(Popsicle popsicle);
        Task UpdateAsync(Popsicle popsicle);
        Task DeleteAsync(Guid id);
    }
}
