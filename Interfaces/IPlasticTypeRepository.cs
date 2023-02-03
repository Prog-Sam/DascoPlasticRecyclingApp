using DascoPlasticRecyclingApp.Models;

namespace DascoPlasticRecyclingApp.Interfaces
{
    public interface IPlasticTypeRepository
    {
        ICollection<PlasticType> GetPlasticTypes();
    }
}
