using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BackOffice.Models;
using BackOffice.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Controllers
{
    /// <summary>
    /// Represents endpoint for upload TSV file
    /// </summary>
    [ApiController]
    [EnableCors("AllAllow")]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly BrandQuantityTimeReceivedRepository brandQuantityTimeReceivedRepository;
        private readonly BrandRepository brandRepository;

        /// <summary>
        ///    Constructor that initializes upload controller
        /// </summary>
        /// <param name="brandRepository"></param>
        /// <param name="brandQuantityTimeReceivedRepository"></param>
        public UploadController(BrandRepository brandRepository,
            BrandQuantityTimeReceivedRepository brandQuantityTimeReceivedRepository)
        {
            this.brandQuantityTimeReceivedRepository = brandQuantityTimeReceivedRepository;
            this.brandRepository = brandRepository;
        }

        /// <summary>
        /// Upload .tsv file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Unsupported file type. The endpoint supports just *.tsv file</exception>
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (!file.FileName.EndsWith(".tsv"))
            {
                throw new ArgumentNullException("file", "Unsupported file type. The endpoint supports just *.tsv files");
            }

            await using (Stream sr = file.OpenReadStream())
            {
                var buffer = new byte[file.Length];

                await sr.ReadAsync(buffer, 0, (int) file.Length);

                var array = Encoding.UTF8.GetString(buffer).Split('\r');

                var columnArray = array[0].Split('\t');

                int nameIndex;
                int quantityIndex;
                
                if (columnArray[0].ToLower() == "name")
                {
                    nameIndex = 0;
                    quantityIndex = 1;
                }
                else
                {
                    nameIndex = 1;
                    quantityIndex = 0;
                }
                
                for (var i = 1; i < array.Length; i++)
                {
                    var row = array[i].Split('\t');

                    var brandName = row[nameIndex];
                    var brandQuantity = Int32.Parse(row[quantityIndex]);

                    var brand = await brandRepository.FindByNameAsync(brandName);
                    if (brand == null)
                    {
                        brand = new Brand
                        {
                            Name = brandName
                        };
                        brandRepository.Add(brand);
                    }

                    var brandQuantityTimeReceived = new BrandQuantityTimeReceived
                    {
                        Brand = brand,
                        Quantity = brandQuantity,
                        TimeReseived = DateTime.Now
                    };
                    brandQuantityTimeReceivedRepository.Add(brandQuantityTimeReceived);
                }

                await brandQuantityTimeReceivedRepository.SaveAsync();
            }

            return Ok();
        }
    }
}