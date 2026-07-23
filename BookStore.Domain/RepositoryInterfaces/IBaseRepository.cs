using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.RepositoryInterfaces;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(long id);

    Task<List<TEntity>> GetAllAsync();

    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);
}