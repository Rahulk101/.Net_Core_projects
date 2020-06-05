using AutoMapper;
using Park_WebApi_Udemy.Models;
using Park_WebApi_Udemy.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Park_WebApi_Udemy.ParkyMapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
        }
    }
}
