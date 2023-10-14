using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FullStack_Task.Constants;
using FullStack_Task.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.FileProviders;
using System;


namespace FullStack_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAll()
        {
            var products = await _unitOfWork.Products.ListAllAsync();
            var productsToReturn = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return Ok(productsToReturn);
        }

        [HttpGet("{id:int}", Name = "AddProduct")]
        public async Task<ActionResult<ProductToReturnDto>> GeById(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            var productToReturn = _mapper.Map<ProductToReturnDto>(product);
            return Ok(productToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<ProductToAddDto>> Add([FromForm] ProductToAddDto product)
        {
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var imageDirectory = Path.Combine(wwwrootPath, StringConstants.ProductImagesDirName);
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
            var imageName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{Path.GetExtension(product.Image.FileName)}";
            var imagePath = Path.Combine(imageDirectory, imageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await product.Image.CopyToAsync(stream);
            }

            var productToAdd = _mapper.Map<Product>(product);
            // Save the image path to database
            var relativeImagePath = Path.Combine(StringConstants.ProductImagesDirName, imageName).Replace("\\", "/");
            productToAdd.ImagePath = relativeImagePath;
            _unitOfWork.Products.Add(productToAdd);
            await _unitOfWork.Complete();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<ActionResult<ProductToEditDto>> Edit([FromForm] ProductToAddDto product, [FromQuery] int productId)
        {
            var productExtension = Path.GetExtension(product.Image.FileName);
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var oldProduct = await _unitOfWork.Products.GetByIdAsync(productId);
            if (oldProduct is null)
            {
                return BadRequest(new { error = "there is no product to update" });
            }
            try
            {
                var imageDirectory = Path.Combine(wwwrootPath, StringConstants.ProductImagesDirName);
                var imageName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{productExtension}";
                var imagePath = Path.Combine(imageDirectory, imageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await product.Image.CopyToAsync(stream);
                }
                var relativeImagePath = Path.Combine(StringConstants.ProductImagesDirName, imageName).Replace("\\", "/");

                var productToEdit = _mapper.Map<Product>(product);
                productToEdit.Id = productId;
                productToEdit.ImagePath = relativeImagePath;

                _unitOfWork.Products.Update(productToEdit);
                await _unitOfWork.Complete();

                //delete img file
                var oldProductImgPath = oldProduct.ImagePath;
                var oldimagePath = Path.Combine(wwwrootPath, oldProductImgPath);
                var fileInfo = new FileInfo(oldimagePath);
                fileInfo.Delete();
            return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productEntity = await _unitOfWork.Products.GetByIdAsync(id);
            if (productEntity is null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.Products.Delete(productEntity);
                _unitOfWork.Complete();
                //delete img file
                var wwwrootPath = _webHostEnvironment.WebRootPath;
                var ProductImgPath = productEntity.ImagePath;
                var oldimagePath = Path.Combine(wwwrootPath, ProductImgPath);
                var fileInfo = new FileInfo(oldimagePath);
                fileInfo.Delete();
                return StatusCode(StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
