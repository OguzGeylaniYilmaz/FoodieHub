using AutoMapper;
using FluentValidation;
using FoodieHub.API.Context;
using FoodieHub.API.Dtos.ProductDtos;
using FoodieHub.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodieHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator<Product> _productValidator;
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public ProductsController(IValidator<Product> productValidator, ApiContext apiContext, IMapper mapper)
        {
            _productValidator = productValidator;
            _apiContext = apiContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _apiContext.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _apiContext.Products.Find(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var validationResult = _productValidator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            _apiContext.Products.Add(product);
            _apiContext.SaveChanges();
            return Ok("Product created successfully.");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _apiContext.Products.Find(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            _apiContext.Products.Remove(product);
            _apiContext.SaveChanges();
            return Ok("Product deleted successfully.");
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var existingProduct = _apiContext.Products.Find(product.ProductID);
            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }
            var validationResult = _productValidator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            _apiContext.Products.Update(product);
            _apiContext.SaveChanges();
            return Ok("Product updated successfully.");
        }

        [HttpPost("CreateProductWithCategory")]
        public IActionResult CreateProduct(CreateProductDto createProduct)
        {
            var product = _mapper.Map<Product>(createProduct);
            var validationResult = _productValidator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            _apiContext.Products.Add(product);
            _apiContext.SaveChanges();
            return Ok("Product created successfully.");
        }

        [HttpGet("ProductListWithCategory")]
        public IActionResult GetProductListWithCategory()
        {
            var product = _apiContext.Products.Include(p => p.Category).ToList();
            return Ok(_mapper.Map<List<ResultProductWithCategory>>(product));
        }
    }
}
