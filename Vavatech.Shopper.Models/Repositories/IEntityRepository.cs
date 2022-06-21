namespace Vavatech.Shopper.Models.Repositories;

// interfejs generyczny
public interface IEntityRepository<TEntity>
{
    IEnumerable<TEntity> Get();
    TEntity Get(int id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(int id);
}
