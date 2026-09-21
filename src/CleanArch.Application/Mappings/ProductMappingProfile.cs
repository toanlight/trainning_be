using AutoMapper;
using CleanArch.Application.DTOs.Product;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Mappings;

/// <summary>
/// AutoMapper profile định nghĩa các mapping rules cho Product.
/// </summary>
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        // Product entity → ProductDto (response)
        CreateMap<Product, ProductDto>();

        // CreateProductRequest → Product entity
        CreateMap<CreateProductRequest, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}
