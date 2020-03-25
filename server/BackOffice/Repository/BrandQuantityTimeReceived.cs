using System.Collections.Generic;
using System.Threading.Tasks;
using BackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Repository
{
    /// <summary>
    /// Represents CRUD operations for a brand quantity change log
    /// </summary>
    public class BrandQuantityTimeReceivedRepository
    {
        private readonly ApplicationDbContext context;
        
        /// <summary>
        /// Constructor that initializes a brand quantity change log repository
        /// </summary>
        /// <param name="context"></param>
        public BrandQuantityTimeReceivedRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        /// <summary>
        /// Get all records of brand quantity change log
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BrandQuantityTimeReceived> GetAll()
        {
            return context.BrandQuantitiesTimeReceived
                .Include(x => x.Brand);
        }
        
        /// <summary>
        /// Add a new record to brand quantity change log
        /// </summary>
        /// <param name="brandQuantitieTimeReceived"></param>
        public void Add(BrandQuantityTimeReceived brandQuantitieTimeReceived)
        {
            context.BrandQuantitiesTimeReceived.Add(brandQuantitieTimeReceived);
        }

        /// <summary>
        /// Commit all app context changes to database
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}