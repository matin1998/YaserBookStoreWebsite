using BookStore.Domain.UnitOfWork;
using BookStore.infrastructure.YaserBookStoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.UnitOfWork;

public class UnitOfWork:IUnitOfWork
{
    private readonly BookStoreDbContext _context;

    public UnitOfWork(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
}
