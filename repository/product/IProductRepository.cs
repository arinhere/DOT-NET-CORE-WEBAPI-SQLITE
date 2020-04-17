using System.Collections.Generic;
using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Repository.product
{
    public interface IProductRepository
    {
        // Create a generic method for add and delete
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        Task<bool> SaveAll();
        Task<IEnumerable<Product>> ListProducts(int userId);
        Task<Product> GetProductById(int id);
    }
}