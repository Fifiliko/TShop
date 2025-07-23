using TShop.Application.DTOs;

namespace TShop.Application.Interfaces.Sevices
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllAsync();
        Task<BrandDto> GetByIdAsync(int id);
        Task<BrandDto> CreateAsync(BrandDto dto);
        Task<BrandDto> UpdateAsync(BrandDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
