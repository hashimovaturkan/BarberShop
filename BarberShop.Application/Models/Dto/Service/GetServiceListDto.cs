using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.Service.Queries;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.Service
{
    public class GetServiceListDto : RequestListTemplate, IMapWith<GetServiceListQuery>
    {
        public void Mapping(Profile profile) =>
            profile.CreateMap<GetServiceListDto, GetServiceListQuery>();
    }
}
