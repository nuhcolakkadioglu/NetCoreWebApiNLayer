using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository _categoryRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : ICategoryService
    {

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryWithProductsAsync(id);
            if (category == null)
                return ServiceResult<CategoryWithProductsDto>.Fail(string.Empty);

            var categoryAsDto = _mapper.Map<CategoryWithProductsDto>(category);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var category = await _categoryRepository.GetCategoryWithProducts().ToListAsync();


            var categoryAsDto = _mapper.Map<List<CategoryWithProductsDto>>(category);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();

            var categoriesAsDto = _mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
        }
        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return ServiceResult<CategoryDto>.Fail("Kayıt bulunamadı");

            var categoriesAsDto = _mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoriesAsDto);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {


            var any = await _categoryRepository.AnyAsync(m => m.Name == request.Name);

            if (any)
                return ServiceResult<int>.Fail("kategori zaten var !");

            var category = _mapper.Map<Category>(request);
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.Success(category.Id);

        }

        public async Task<ServiceResult<int>> UpdateAsync(UpdateCategoryRequest request)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category is null)
                return ServiceResult<int>.Fail("Kayıt bulunamadı");


            var any = await _categoryRepository.AnyAsync(m => m.Name == request.Name & m.Id != request.Id);
            if (any)
                return ServiceResult<int>.Fail("kategori zaten var !");

            var categoryMap = _mapper.Map(request, category);

            _categoryRepository.Update(categoryMap);

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.Success(category.Id);

        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return ServiceResult.Fail("Kayıt bulunamadı");

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(httpStatusCode: System.Net.HttpStatusCode.NoContent);


        }


    }
}
