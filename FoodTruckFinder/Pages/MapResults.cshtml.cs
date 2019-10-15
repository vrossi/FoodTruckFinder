using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckFinder.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BingMapsRESTToolkit;
using FoodTruckFinder.Extensions;

namespace FoodTruckFinder.Pages
{
    public class MapResultsModel : PageModel
    {
        private readonly IMobileFoodService _mobileFoodService;

        public MapResultsModel(IMobileFoodService mobileFoodService)
        {
            _mobileFoodService = mobileFoodService;
        }

        public List<MobileFoodFacility> FoodTrucks
        {
            get;
            set;
        }

        public SearchAddressCoordinates SearchAddressLocation
        {
            get;
            set;
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            var request = new GeocodeRequest()
            {
                BingMapsKey = "AmJ4AgkRYthF9V-QFXQXtdcTQO3tfKcfAeuOMJm0IPv2HGVrETieIpnv33sE2H7-",
                Query = Request.Form["searchAddress"]
            };

            var r = await ServiceManager.GetResponseAsync(request);

            if (!(r != null && r.ResourceSets != null &&
                r.ResourceSets.Length > 0 &&
                r.ResourceSets[0].Resources != null &&
                r.ResourceSets[0].Resources.Length > 0))
            {
                throw new Exception("No results found.");
            }

            var location = r.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Location;

            SearchAddressLocation = new SearchAddressCoordinates
            {
                Latitude = location.Point.Coordinates[0],
                Longitude = location.Point.Coordinates[1]
            };

            var closestFoodTrucks = await _mobileFoodService.GetClosestMobileFoodFacility(FacilityType.FoodTruck, location.Point.Coordinates[0], location.Point.Coordinates[1]);

            FoodTrucks = closestFoodTrucks.DistinctBy(ft => ft.Latitude, ft => ft.Longitude).Take(5).Select(ft => new MobileFoodFacility
            {
                Id = ft.Id,
                Address = ft.Address,
                Applicant = ft.Applicant,
                FoodItems = ft.FoodItems,
                Latitude = ft.Latitude,
                Longitude = ft.Longitude
            }).ToList();
        }
    }

    public class SearchAddressCoordinates
    {
        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }
    }

    public class MobileFoodFacility
    { 
        public string Id
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string FoodItems
        {
            get;
            set;
        }

        public string Applicant
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }
    }
}
