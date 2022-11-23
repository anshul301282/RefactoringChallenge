using AutoMapper;
using RefactoringChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactoringChallenge.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Order, OrderRequest>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailRequest>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailResponse>().ReverseMap();
        }
    }
}
