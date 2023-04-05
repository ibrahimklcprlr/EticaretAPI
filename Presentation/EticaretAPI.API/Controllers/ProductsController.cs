
using EticaretAPI.Aplication.Features.Commands.Product.CreateProduct;
using EticaretAPI.Aplication.Features.Commands.Product.DeleteProduct;
using EticaretAPI.Aplication.Features.Commands.Product.UpdateProduct;
using EticaretAPI.Aplication.Features.Commands.ProductImageFile.ChangeShowCaseImageFile;
using EticaretAPI.Aplication.Features.Commands.ProductImageFile.DeleteProductImageFile;
using EticaretAPI.Aplication.Features.Commands.ProductImageFile.UploadProductImageFile;
using EticaretAPI.Aplication.Features.Queries.Product.GetAllProduct;
using EticaretAPI.Aplication.Features.Queries.Product.GetProduct;
using EticaretAPI.Aplication.Features.Queries.ProductImageFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class ProductsController : ControllerBase
    {
    
        readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }
  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]GetByIdProductQueryRequest request )
        {
            GetByIdProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<ActionResult> Update([FromBody]UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest request)
        {
            DeleteProductCommandResponse response =await _mediator.Send(request);
            return Ok();
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload([FromQuery]UploadProductImageFileCommandRequest request)
        {
            request.FormData = Request.Form.Files;
            UploadProductImageFileCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetProductImages([FromRoute]GetProductImageFileQueryRequest request)
        {
            List <GetProductImageFileQueryResponse> response = await _mediator.Send(request);
            return Ok(response);

        }
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]DeleteProductImageFileCommandRequest request, [FromQuery] string ImageId)
        {
            request.ImageId = ImageId;
            DeleteProductImageFileCommandResponse response = await _mediator.Send(request);
            return Ok();

        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes ="Admin")]
        public async Task<IActionResult> ChangeShowCaseImageFile([FromQuery]ChangeShowCaseImageFileCommandRequest request)
        {
            ChangeShowCaseImageFileCommandResponse response =await _mediator.Send(request);
            return Ok(response);
        }
    }
}
