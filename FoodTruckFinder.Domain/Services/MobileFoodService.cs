using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FoodTruckFinder.Domain.ValueObjects;
using GeoCoordinatePortable;
using Microsoft.Extensions.Caching.Memory;
using NodaTime;

namespace FoodTruckFinder.Domain.Services
{
    public enum FacilityType
    {
        FoodTruck = 1,
        PushCart = 2
    }

    public class MobileFoodService : IMobileFoodService
    {
        private const string FacilitiesCacheKey = "MobileFoodService.Facilities";
        private const string ValidStatus = "APPROVED";

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly IClock _clock;

        public MobileFoodService(
            HttpClient httpClient,
            IMemoryCache memoryCache,
            IClock clock)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _clock = clock;
        }

        public async Task<List<MobileFoodFacility>> GetClosestMobileFoodFacility(FacilityType facilityType, double latitide, double longitude)
        {
            var mobileFoodFascilities = await EnsureFacilitiesAreCachedAsync();

            return (from ff in mobileFoodFascilities
                    let distance = (3959 * Math.Acos(Math.Cos(DegreeToRadian(latitide)) * Math.Cos(DegreeToRadian(ff.Latitude)) * Math.Cos(DegreeToRadian(ff.Longitude) - DegreeToRadian(longitude)) + Math.Sin(DegreeToRadian(latitide)) * Math.Sin(DegreeToRadian(ff.Latitude))))
                    where ff.Status == ValidStatus && distance < 5
                    orderby distance
                    select ff).ToList();
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private async Task<List<MobileFoodFacility>> EnsureFacilitiesAreCachedAsync()
        {
            return await _memoryCache.GetOrCreateAsync<List<MobileFoodFacility>>(FacilitiesCacheKey, async entry =>
            {
                // Cache for 1 hour
                entry.AbsoluteExpiration = _clock.GetCurrentInstant().PlusTicks(new TimeSpan(1, 00, 00).Ticks).ToDateTimeOffset();

                return await this.GetAllMobileFoodFacilities();
            });
        }

        private async Task<List<MobileFoodFacility>> GetAllMobileFoodFacilities()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/odata/v4/rqzj-sfat");

            using (var response = await _httpClient.SendAsync(requestMessage))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();

                    throw new Exception(errorDetails);
                }

                var mobileFacilityResponse = await response.Content.ReadAsAsync<MobileFacilityResponse>();

                mobileFacilityResponse.Values.ForEach(mff => new GeoCoordinate(mff.Latitude, mff.Longitude));

                return mobileFacilityResponse.Values;
            }
        }
    }
}
