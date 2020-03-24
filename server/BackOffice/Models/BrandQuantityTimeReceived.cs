using System;

namespace BackOffice.Models
{
    /// <summary>
    /// BrandQuantityTimeReceived entity. Log all quantity changes in back office
    /// </summary>
    public class BrandQuantityTimeReceived
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Reference to Brand
        /// </summary>
        public Brand Brand { get; set; }
        
        /// <summary>
        /// Quantity of brand inventory 
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Time when changes were made
        /// </summary>
        public DateTime TimeReseived { get; set; }
    }
}