using App.Application.Contracts.Persistence;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using AutoMapper;
using System.Net;
using App.Application.Contracts.Caching;
namespace App.Application.Features.Products;

public class ProductService(
    IProductRepository _productRepository,
    IUnitOfWork _unitOfWork,
    IMapper _mapper,
    ICacheService _cacheService) : IProductService
{

    private const string productListCacheKey = "productListCacheKey";
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsyn(int count)
    {
        var data = await _productRepository.GetTopPriceProductsAsyn(count);


        var result = _mapper.Map<List<ProductDto>>(data); // data.Select(m => new ProductDto(m.Id, m.Name, m.Price, m.Stock)).ToList();


        return ServiceResult<List<ProductDto>>.Success(result);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {

        var productListAsCached = await _cacheService.GetAsync<List<ProductDto>>(productListCacheKey);

        if (productListAsCached is not null)
            return ServiceResult<List<ProductDto>>.Success(productListAsCached);


        var data = await _productRepository.GetAllAsync();
        var productAsDto = _mapper.Map<List<ProductDto>>(data);
        await _cacheService.AddAsync(productListCacheKey, productAsDto, TimeSpan.FromMinutes(10));

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        var products = await _productRepository.GetAllPagedAsync(pageNumber, pageSize);

        var productAsDto = _mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productAsDto);

    }
    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
            return ServiceResult<ProductDto?>.Fail(errorrMessage: "product bulunamadı", HttpStatusCode.NotFound);

        var productAsDto = _mapper.Map<ProductDto>(product);

        return ServiceResult<ProductDto?>.Success(productAsDto);
    }
    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        //        throw new CriticalException("CriticalException ateş ettti");
        var anyProduct = await _productRepository.AnyAsync(m => m.Name == request.Name);

        if (anyProduct)
            return ServiceResult<CreateProductResponse>.Fail("Ürün zaten var !");

        var product = _mapper.Map<Product>(request);

        await _productRepository.AddAsync(product);
        int result = await _unitOfWork.SaveChangesAsync();

        return result > 0 ? ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}")
                         : ServiceResult<CreateProductResponse>.Fail("kayıt işlemi başarısız oldu");

    }
    public async Task<ServiceResult> UpdateAsync(UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
            return ServiceResult.Fail("Kayıt bulunamadı");

        var anyProduct = await _productRepository

            .AnyAsync(m => m.Name == request.Name && m.Id != product.Id);

        if (anyProduct)
            return ServiceResult.Fail("bu isimde ürün daha önce eklenmiş", HttpStatusCode.BadRequest);


        product = _mapper.Map(request, product);

        _productRepository.Update(product);
        int result = await _unitOfWork.SaveChangesAsync();

        return result > 0 ? ServiceResult.Success(HttpStatusCode.NoContent)
                            : ServiceResult.Fail("güncelleme başarısız oldu");

    }
    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest updateProductStockRequest)
    {
        var product = await _productRepository.GetByIdAsync(updateProductStockRequest.ProductId);

        if (product is null)
            return ServiceResult.Fail("ürün bulunamadı", HttpStatusCode.NotFound);

        product.Stock = updateProductStockRequest.Quantity;
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);

    }
    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            return ServiceResult.Fail("Kayıt bulunamadı");

        _productRepository.Delete(product);
        int result = await _unitOfWork.SaveChangesAsync();

        return result > 0 ? ServiceResult.Success(HttpStatusCode.NoContent)
                           : ServiceResult.Fail("silme başarısız oldu");
    }

}
