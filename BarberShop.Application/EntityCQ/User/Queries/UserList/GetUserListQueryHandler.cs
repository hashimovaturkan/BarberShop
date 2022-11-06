using BarberShop.Application.Models.Template;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.User.Queries.UserList
{
    public class GetUserListQuery : RequestListQueryTemplate
    {
        public string? SearchingWord { get; set; }
    }
}
