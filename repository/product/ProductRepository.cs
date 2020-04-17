using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOT_NET_CORE_WEBAPI_SQLITE.Data;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;
using Microsoft.EntityFrameworkCore;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Repository.product
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDataContext _data;
        public ProductRepository(AppDataContext data)
        {
            _data = data;
        }
        public void Add<T>(T entity) where T : class
        {
            _data.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _data.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            // it will return true if more than one save changes, otherwise false
            return await _data.SaveChangesAsync() > 0;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _data.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> ListProducts(int userId)
        {
            var products = await _data.Products.Where(p => p.UserId == userId).ToListAsync();
            return products;
        }
    }
}