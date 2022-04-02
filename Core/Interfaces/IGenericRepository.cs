using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithoutTrackingAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();
        void Add(T entity);

        void Update(T entity);

        void Attach(T entity);
        void Add(IEnumerable<T> entity);
        Task AddAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);

        void Delete(T entity);
        Task<T> GetFirstOrDefaultBySpecAsync(ISpecification<T> spec);

        Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> spec);
    }
}
