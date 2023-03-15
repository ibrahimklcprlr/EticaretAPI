using Azure.Core;
using EticaretAPI.Aplication.Abstraction.Storage;
using EticaretAPI.Aplication.Features.Commands.CreateProduct;
using EticaretAPI.Aplication.Features.Queries.GetAllProduct;
using EticaretAPI.Aplication.Repositories;
using EticaretAPI.Aplication.RequestParametres;
using EticaretAPI.Aplication.ViewModels;
using EticaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Runtime.CompilerServices;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webhostEnviroment;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IInvoiceFileReadRepository _ınvoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository _ınvoiceFileWriteRepository;
        readonly IStorageService _storageService;
        readonly IConfiguration configuration;
        readonly IMediator _mediator;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webhostEnviroment, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IInvoiceFileReadRepository ınvoiceFileReadRepository, IInvoiceFileWriteRepository ınvoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webhostEnviroment = webhostEnviroment;

            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _ınvoiceFileReadRepository = ınvoiceFileReadRepository;
            _ınvoiceFileWriteRepository = ınvoiceFileWriteRepository;
            _storageService = storageService;
            this.configuration = configuration;
            _mediator = mediator;
            //_mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }
  
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //Product p = await _productReadRepository.GetByIdAsync(id);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string filename, string pathorContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id, true);
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.filename,
                Path = r.pathorContainerName,
                Storage = _storageService.StorageName,
                Product = new List<Product>() { product }
            }).ToList()); ;
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            return Ok(product.ProductImages.Select(p => new
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            }));
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            ProductImageFile productImageFile = product.ProductImages.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            product.ProductImages.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();
            return Ok();

        }
    }
}
