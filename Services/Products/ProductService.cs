using App.Repositories;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace App.Services.Products;

public class ProductService(IProductRepository _productRepository, IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsyn(int count)
    {
        var data = await _productRepository.GetTopPriceProductsAsyn(count);

        var result = data.Select(m => new ProductDto(m.Id, m.Name, m.Price, m.Stock)).ToList();


        return ServiceResult<List<ProductDto>>.Success(result);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var data = await _productRepository.GetAll().ToListAsync();
        var productAsDto = _mapper.Map<List<ProductDto>>(data);
        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
    {
        var products = await _productRepository.GetAll()
                                               .Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToListAsync();

        var result = products.Select(m => new ProductDto(m.Id, m.Name, m.Price, m.Stock)).ToList();
        var productAsDto = _mapper.Map<List<ProductDto>>(result);

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

        var productAsDto = _mapper.Map<Product>(request);


        _productRepository.Update(productAsDto);
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
