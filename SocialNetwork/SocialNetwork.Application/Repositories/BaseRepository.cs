using SocialNetwork.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace SocialNetwork.Application.Repositories
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly IApplicationDbContext _applicationDbContext;

        public BaseRepository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Create(T obj)
        {
            _applicationDbContext.Set<T>().Add(obj);
        }

        public T? Find(Expression<Func<T, bool>> predicate)
        {
            return _applicationDbContext.Set<T>().FirstOrDefault(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _applicationDbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _applicationDbContext.Set<T>().Find(id)!;
        }

        public virtual IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _applicationDbContext.Set<T>().Where(predicate).ToList()!;
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(T obj)
        {
            _applicationDbContext.Set<T>().Update(obj);
        }
    }
}