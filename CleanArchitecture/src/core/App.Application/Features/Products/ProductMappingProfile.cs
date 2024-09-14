﻿using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequest, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToUpper()));

            CreateMap<UpdateProductRequest, Product>()
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToUpper()));
        }
    }
}
