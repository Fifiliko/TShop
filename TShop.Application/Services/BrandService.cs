using AutoMapper;
using TShop.Application.DTOs;
using TShop.Application.Interfaces.Repositories;
using TShop.Application.Interfaces.Sevices;
using TShop.Domain.Entities;

namespace TShop.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<BrandDto> CreateAsync(BrandDto dto)
        {
            var brand = _mapper.Map<Brand>(dto);
            await _brandRepository.AddAsync(brand);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<BrandDto> UpdateAsync(BrandDto dto)
        {
            var brand = _mapper.Map<Brand>(dto);
            await _brandRepository.UpdateAsync(brand);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _brandRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _brandRepository.DeleteAsync(id);
            return true;
        }
    }

}
