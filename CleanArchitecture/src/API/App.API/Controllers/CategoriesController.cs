using App.API.Filters;
using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.update;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{

    public class CategoriesController(ICategoryService _categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
               => CreateActionResult(await _categoryService.GetAllListAsync());

        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProductsAsync()
               => CreateActionResult(await _categoryService.GetCategoryWithProductsAsync());

        [HttpGet("{id:int}/products")]
        public async Task<IActionResult> GetCategoryWithProductsAsync(int id)
               => CreateActionResult(await _categoryService.GetCategoryWithProductsAsync(id));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
               => CreateActionResult(await _categoryService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest model)
               => CreateActionResult(await _categoryService.CreateAsync(model));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryRequest model)
               => CreateActionResult(await _categoryService.UpdateAsync(model));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
             => CreateActionResult(await _categoryService.DeleteAsync(id));
    }
}
