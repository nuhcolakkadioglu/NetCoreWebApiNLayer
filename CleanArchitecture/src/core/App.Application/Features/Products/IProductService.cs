using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;

namespace App.Application.Features.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsyn(int count);
        Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
        Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
        Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateAsync(UpdateProductRequest request);
        Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest updateProductStockRequest);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
