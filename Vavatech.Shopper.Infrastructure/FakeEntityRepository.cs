using Bogus;
using Microsoft.Extensions.Options;
using Vavatech.Shopper.Models;
using Vavatech.Shopper.Models.Repositories;

namespace Vavatech.Shopper.Infrastructure
{
    public class FakeEntityOptions
    {
        public int Count { get; set; }
    }

    public class FakeEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IDictionary<int, TEntity> entities;

        public FakeEntityRepository(Faker<TEntity> faker, IOptions<FakeEntityOptions> options)
        {
            entities = faker.Generate(options.Value.Count).ToDictionary(p => p.Id);
        }

        public virtual void Add(TEntity entity)
        {
            var id = entities.Max(c => c.Key);
            entity.Id = ++id;
            entities[entity.Id] = entity;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return entities.Values;
        }

        public virtual TEntity Get(int id)
        {
            if (entities.TryGetValue(id, out TEntity entity))
            {
                return entity;
            }

            return null;
        }

        public virtual void Remove(int id)
        {
            entities.Remove(id);
        }

        public virtual void Update(TEntity entity)
        {
            entities[entity.Id] = entity;
        }
    }
}