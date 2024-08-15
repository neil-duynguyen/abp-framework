using Acme.BookStore.CarDto;
using Acme.BookStore.Dto;
using AutoMapper;
using ImportSample.Colors;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleBrands;
using ImportSample.VehicleCategories;
using ImportSample.VehicleEngines;
using ImportSample.VehicleModels;
using ImportSample.VehicleModelStyles;
using ImportSample.VehicleYearModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore
{
    public class BookAutoMapperProfile : Profile
    {
        public BookAutoMapperProfile() { 
            CreateMap<BookCreateDto, Book>().ReverseMap();
            CreateMap<BookViewDto, Book>().ReverseMap();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
            CreateMap<BookViewDto, ElasticsearchDto>().ReverseMap();

            CreateMap<ColorViewDto, Color>().ReverseMap();
            CreateMap<VehicleModelStyleDto, VehicleModelStyle>().ReverseMap();
            CreateMap<VehicleBrandDto, VehicleBrand>().ReverseMap();
            CreateMap<VehicleModelDto, VehicleModel>().ReverseMap();
            CreateMap<VehicleYearModelDto, VehicleYearModel>().ReverseMap();
            CreateMap<VehicleCategoryDto, VehicleCategory>().ReverseMap();
            CreateMap<VehicleEngineDto, VehicleEngine>().ReverseMap();
        }
    }
}
