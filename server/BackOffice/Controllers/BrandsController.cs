using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackOffice.Models;
using BackOffice.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using BrandQuantityTimeReceived = BackOffice.Models.BrandQuantityTimeReceived;

namespace BackOffice.Controllers
{
    /// <summary>
    /// Represents endpoint for working with brand entity
    /// </summary>
    [ApiController]
    [EnableCors("AllAllow")]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly BrandRepository brandRepository;
        private readonly BrandQuantityTimeReceivedRepository brandQuantityTimeReceivedRepository;

        /// <summary>
        ///  Constructor that initializes brand controller
        /// </summary>
        /// <param name="brandRepository"></param>
        /// <param name="brandQuantityTimeReceivedRepository"></param>
        public BrandsController(BrandRepository brandRepository,
            BrandQuantityTimeReceivedRepository brandQuantityTimeReceivedRepository)
        {
            this.brandRepository = brandRepository;
            this.brandQuantityTimeReceivedRepository = brandQuantityTimeReceivedRepository;
        }

        /// <summary>
        /// Gets a specific brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Specific brand</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">If the brand doesn't exist</response>  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var brand = await brandRepository.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        /// <summary>
        /// Gets all brands
        /// </summary>
        /// <returns>Collection of brands</returns>
        /// <response code="200">Ok</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = brandQuantityTimeReceivedRepository.GetAll().GroupBy(x => x.Brand.Name).Select(x => new
            {
                Id = x.First().Brand.Id,
                Name = x.Key.Trim(),
                NumberOfUploads = x.Count(),
                Sum = x.Sum(y => y.Quantity)
            }).ToList();

            return Ok(brands);
        }

        /// <summary>
        /// Create a new brand with metadata like [TimeReseived] and [Quantity]
        /// </summary>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <returns>A newly created brand</returns>
        /// <response code="201">Created</response>
        /// <response code="400"></response>  
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] int quantity, [FromForm] string name)
        {
            var brand = new Brand
            {
                Name = name,
            };

            var brandQuantityTimeReceived = new BrandQuantityTimeReceived
            {
                Brand = brand,
                Quantity = quantity,
                TimeReseived = DateTime.Now
            };

            brandRepository.Add(brand);
            brandQuantityTimeReceivedRepository.Add(brandQuantityTimeReceived);

            if (await brandRepository.SaveAsync() == 0)
            {
                return BadRequest();
            }

            return CreatedAtAction("Post", brand);
        }

        /// <summary>
        /// Update brand quantity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <response code="204">No Content</response>
        /// <response code="400">If no success save to database</response>
        /// <response code="404">If a brand doesn't exist</response>  
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromForm] int quantity)
        {
            var brand = await brandRepository.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            var brandQuantityTimeReceived = new BrandQuantityTimeReceived
            {
                Brand = brand,
                Quantity = quantity,
                TimeReseived = DateTime.Now
            };

            brandQuantityTimeReceivedRepository.Add(brandQuantityTimeReceived);

            if (await brandRepository.SaveAsync() == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Delete specific brand with all related objects
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Ok</response>
        /// <response code="400">If no success save to database</response>
        /// <response code="404">If a brand doesn't exist</response>  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var brand = await brandRepository.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            brandRepository.Remove(brand);

            if (await brandRepository.SaveAsync() == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Get sum of Inventory for all Brands.
        /// </summary>
        /// <returns>Sum</returns>
        /// <response code="200">Ok</response>
        [HttpGet]
        [Route("sum")]
        public IActionResult GetSum()
        {
            var sum = brandQuantityTimeReceivedRepository.GetAll().Sum(x => x.Quantity);
            return Ok(sum);
        }
    }
}