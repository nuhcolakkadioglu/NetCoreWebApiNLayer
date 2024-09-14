using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.update;

namespace App.Application.Features.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int id);
        Task<ServiceResult<int>> UpdateAsync(UpdateCategoryRequest request);
    }
}