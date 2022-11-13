﻿using AutoMapper;
using BarberShop.Application.Common.Mappings;
using BarberShop.Application.Models.Vm.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Models.Vm.Filial
{
    public class FilialDetailsVm : IMapWith<Domain.Filial>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lang { get; set; }
        public string Long { get; set; }
        public string? ImageUrl { get; set; }
        public int? PhotoId { get; set; }
        public string? Address { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Filial, FilialDetailsVm>();
        }
    }
}
