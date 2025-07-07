using System.Linq.Expressions;

namespace CinemaApp.Data.Repository.Contracts;

public interface IRepository<TType, TId>
{
    TType? GetById(TId id);

    Task<TType?> GetByIdAsync(TId id);

    TType? FirstOrDefault(Func<TType, bool> predicate);

    Task<TType?> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);

    Task<TType?> FirstOrDefaultReadonlyAsync(Expression<Func<TType, bool>> predicate);

    Task<ICollection<TType>> GetCollectionByConditionsReadonlyAsync(Expression<Func<TType, bool>> predicate);

    IEnumerable<TType> GetAllReadonly();

    Task<IEnumerable<TType>> GetAllReadonlyAsync();

    IQueryable<TType> GetAllAttached();

    void Add(TType item);

    Task AddAsync(TType item);

    void AddRange(TType[] items);

    Task AddRangeAsync(TType[] items);

    bool Delete(TType entity);

    Task<bool> DeleteAsync(TType entity);

    bool Update(TType item);

    Task<bool> UpdateAsync(TType item);

    Task<TType?> FindByConditionsAsync(Expression<Func<TType, bool>> predicate);

    Task SaveChangesAsync();
}