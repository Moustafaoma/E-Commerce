namespace E_Commerce.APIs.Helpers;
using AutoMapper;
using E_Commerce.APIs.DTOs;
using E_Commerce.Core.Models;

public class MappingProfiles:Profile
{
	private readonly string _baseImageUrl;
	public MappingProfiles(IConfiguration configuration)
	{
		_baseImageUrl = configuration["applicationUrl"];
		CreateMap<Product, ProductToReturnDto>()
			.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
			.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
			.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => $"{_baseImageUrl}/{src.PictureUrl}"));


	}
    
}

