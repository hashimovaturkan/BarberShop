using AutoMapper;
using BarberShop.Application.Common.Components;
using BarberShop.Application.Common.Extensions;
using BarberShop.Application.EntityCQ.Filial.Commands;
using BarberShop.Application.EntityCQ.Filial.Interfaces;
using BarberShop.Application.Models.Vm.Filial;
using BarberShop.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Filial.Services
{
    public class FilialService : IFilialService
    {
        private readonly BarberShopDbContext _dbContext;
        private readonly IMapper _mapper;
        readonly IHttpContextAccessor httpContextAccessor;
        public FilialService(IHttpContextAccessor httpContextAccessor,BarberShopDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
        }

        public async Task<int> Create(CreateFilialCommand dto)
        {
            Domain.Filial filial = _mapper.Map<Domain.Filial>(dto);
            if (dto.Image != null)
            {
                BarberShop.Domain.Photo photo = new BarberShop.Domain.Photo();
                var image = dto.Image.ConvertFile();



                photo = new()
                {
                    Name = image.File.FileName,
                    Path = image.Path,
                    CreatedDate = DateTime.UtcNow,
                    CreatedIp = "::1"
                };

                filial.Photo = photo;
            }
                
            await _dbContext.Filials.AddAsync(filial, CancellationToken.None);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return filial.Id;
        }

        public async Task<FilialDetailsVm> Get(int Id)
        {
            var filial = await _dbContext.Filials.Where(e => e.IsActive && e.Id == Id).FirstOrDefaultAsync();

            var vm = _mapper.Map<FilialDetailsVm>(filial);

            if (vm.PhotoId != null)
                vm.ImageUrl = httpContextAccessor.GeneratePhotoUrl((int)vm.PhotoId);

            return vm;
        }

        public async Task<List<FilialListDto>> GetList()
        {
            var filials = await _dbContext.Filials.Where(e => e.IsActive).ToListAsync();

            var vm = _mapper.Map<List<FilialListDto>>(filials);

            foreach (var filial in vm)
            {
                if (filial.PhotoId != null)
                    filial.ImageUrl = httpContextAccessor.GeneratePhotoUrl((int)filial.PhotoId);

            }

            return vm;
        }
    }
}
