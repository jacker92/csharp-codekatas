using System.Linq.Expressions;

namespace SocialNetwork.Application.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        void Create(T obj);
        void Update(T obj);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        void Save();
        T? Find(Expression<Func<T, bool>> predicate);
    }
}