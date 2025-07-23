using TShop.Application.Interfaces.Repositories;
using TShop.Domain.Entities;
using TShop.Infrustructure.Data;
using TShop.Infrustructure.Repositories.Base;

namespace TShop.Infrustructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
        }
    }
}
