
using AutoMapper;
using Greggs.Products.Api.Models;
using System;

namespace Greggs.Products.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductEuroDto>()
                .ForMember(dest => dest.PriceInEuros,
                           opt => opt.MapFrom(src => Math.Round(src.PriceInPounds * 1.11m, 2)));
        }
    }
}
