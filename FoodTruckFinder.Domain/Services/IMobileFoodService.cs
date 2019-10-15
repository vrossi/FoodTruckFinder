using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTruckFinder.Domain.ValueObjects;

namespace FoodTruckFinder.Domain.Services
{
    public interface IMobileFoodService
    {
        Task<List<MobileFoodFacility>> GetClosestMobileFoodFacility(FacilityType facilityType, double latitide, double longitude);
    }
}
