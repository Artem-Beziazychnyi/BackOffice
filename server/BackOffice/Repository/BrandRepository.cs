using System.Threading.Tasks;
using BackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Repository
{
    /// <summary>
    /// Represents CRUD operations for a brand entity
    /// </summary>
    public class BrandRepository
    {
        private readonly ApplicationDbContext context;
        
        /// <summary>
        /// Constructor that initializes a brand repository
        /// </summary>
        /// <param name="context"></param>
        public BrandRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Find specific brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Brand> FindAsync(int id)
        {
            return await context.Brands.SingleOrDefaultAsync(b => b.Id == id);
        }
        
        /// <summary>
        /// Find a specific brand by id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Brand> FindByNameAsync(string name)
        {
            return await context.Brands.SingleOrDefaultAsync(b => b.Name == name);
        }
        
        /// <summary>
        /// Add a newly created brand
        /// </summary>
        /// <param name="brand"></param>
        public void Add(Brand brand)
        {
            context.Brands.Add(brand);
        }
        
        /// <summary>
        /// Remove a specific brand
        /// </summary>
        /// <param name="brand"></param>
        public void Remove(Brand brand)
        {
            context.Brands.Remove(brand);
        }

        /// <summary>
        /// Commit all app context changes to database
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}