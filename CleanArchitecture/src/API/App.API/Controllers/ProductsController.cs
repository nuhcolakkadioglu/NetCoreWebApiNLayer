
using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{

    public class ProductsController(IProductService _productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
                => CreateActionResult(await _productService.GetAllListAsync());

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
                => CreateActionResult(await _productService.GetPagedAllListAsync(pageNumber, pageSize));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
                 => CreateActionResult(await _productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest model)
                => CreateActionResult(await _productService.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest model)
               => CreateActionResult(await _productService.UpdateAsync(model));

        [HttpPatch("stock")]
        public async Task<IActionResult> Update(UpdateProductStockRequest model)
               => CreateActionResult(await _productService.UpdateStockAsync(model));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
              => CreateActionResult(await _productService.DeleteAsync(id));


    }
}
