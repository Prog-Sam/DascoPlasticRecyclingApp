using DascoPlasticRecyclingApp.Interfaces;
using DascoPlasticRecyclingApp.Models;

namespace DascoPlasticRecyclingApp.Repository
{
    public class PlasticTypeRepository : IPlasticTypeRepository
    {
        private readonly AppDbContext _context;
        public PlasticTypeRepository(AppDbContext context)
        {
            _context= context;
        }

        public ICollection<PlasticType> GetPlasticTypes()
        {
            return _context.PlasticTypes.OrderBy(p => p.Id).ToList();
        }
    }
}
