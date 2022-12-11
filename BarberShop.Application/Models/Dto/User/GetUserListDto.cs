using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.EntityCQ.User.Queries.UserList;
using BarberShop.Application.Models.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Dto.User
{
    public class GetUserListDto : RequestListTemplate, IMapWith<GetUserListQuery>
    {

        public void Mapping(Profile profile) =>
            profile.CreateMap<GetUserListDto, GetUserListQuery>();
    }
}
