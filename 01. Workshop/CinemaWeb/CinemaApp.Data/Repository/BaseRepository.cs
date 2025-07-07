using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CinemaApp.Data.Repository;

public abstract class BaseRepository<TType, TId> : IRepository<TType, TId> 
    where TType : class
    where TId : notnull
{
    private readonly CinemaAppDbContext _dbContext;
    private readonly DbSet<TType> _dbSet;

    public BaseRepository(CinemaAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TType>();
    }

    public TType? GetById(TId id)
    {
        TType? entity = _dbSet
            .Find(id);

        return entity;
    }

    public async Task<TType?> GetByIdAsync(TId id)
    {
        TType? entity = await _dbSet
            .FindAsync(id);

        return entity;
    }

    public TType? FirstOrDefault(Func<TType, bool> predicate)
    {
        TType? entity = _dbSet
            .FirstOrDefault(predicate);

        return entity;
    }

    public async Task<TType?> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate)
    {
        TType? entity = await _dbSet
            .FirstOrDefaultAsync(predicate);

        return entity;
    }

    public async Task<TType?> FirstOrDefaultReadonlyAsync(Expression<Func<TType, bool>> predicate)
    {
        TType? entity = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);

        return entity;
    }

    public IEnumerable<TType> GetAllReadonly() => _dbSet.AsNoTracking().ToArray();

    public async Task<IEnumerable<TType>> GetAllReadonlyAsync() => await _dbSet.AsNoTracking().ToArrayAsync();
    
    public IQueryable<TType> GetAllAttached() => _dbSet.AsQueryable();
    
    public void Add(TType item)
    {
        _dbSet.Add(item);
        _dbContext.SaveChanges();
    }

    public async Task AddAsync(TType item)
    {
        await _dbSet.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public void AddRange(TType[] items)
    {
        _dbSet.AddRange(items);
        _dbContext.SaveChanges();
    }

    public async Task AddRangeAsync(TType[] items)
    {
        await _dbSet.AddRangeAsync(items);
        await _dbContext.SaveChangesAsync();
    }

    public bool Delete(TType entity)
    {
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();

        return true;
    }

    public async Task<bool> DeleteAsync(TType entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public bool Update(TType item)
    {
        try
        {
            _dbSet.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(TType item)
    {
        try
        {
            _dbSet.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<TType?> FindByConditionsAsync(Expression<Func<TType, bool>> predicate)
    {
        TType? entity = await _dbSet
            .FirstOrDefaultAsync(predicate);

        return entity;
    }

    public async Task<ICollection<TType>> GetCollectionByConditionsReadonlyAsync(Expression<Func<TType, bool>> predicate)
    {
        ICollection<TType> entity = await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .ToArrayAsync();

        return entity;
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    
}