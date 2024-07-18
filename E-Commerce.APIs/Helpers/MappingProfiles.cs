namespace E_Commerce.APIs.Helpers;
using AutoMapper;
using E_Commerce.APIs.DTOs;
using E_Commerce.Core.Models;

public class MappingProfiles:Profile
{
	public MappingProfiles()
	{
		CreateMap<Product, ProductToReturnDto>()
			.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
			.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

	}
}

