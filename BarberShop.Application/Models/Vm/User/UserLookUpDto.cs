﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.User
{
    public class UserLookUpDto
    {
        public List<UserListVm> Users { get; set; }
        public int Count { get; set; }
    }
}
