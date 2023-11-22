using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multitenant.Example.Application.Abstraction;
using Multitenant.Example.Application.ViewModels;

namespace Multitenant.Example.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductVM createProductVM)
        {
            return Ok(await _productService.CreateAsync(createProductVM.Name, createProductVM.Description, createProductVM.Rate));
        }
    }
}
