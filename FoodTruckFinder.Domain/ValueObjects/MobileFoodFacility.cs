using System.Collections.Generic;
using GeoCoordinatePortable;
using Newtonsoft.Json;

namespace FoodTruckFinder.Domain.ValueObjects
{
    public class MobileFacilityResponse
    {
        [JsonProperty("odatacontext")]
        public string ODataContext { get; set; }

        [JsonProperty("value")]
        public List<MobileFoodFacility> Values { get; set; }
    }

    public class MobileFoodFacility
    {
        [JsonProperty("__id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public int ObjectId { get; set; }

        [JsonProperty("applicant")]
        public string Applicant { get; set; }

        [JsonProperty("FacilityType")]
        public string Facilitytype { get; set; }

        [JsonProperty("locationdescription")]
        public string LocationDescription { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("blocklot")]
        public string BlockLot { get; set; }

        [JsonProperty("block")]
        public string Block { get; set; }

        [JsonProperty("lot")]
        public string Lot { get; set; }

        [JsonProperty("permit")]
        public string Permit { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("fooditems")]
        public string FoodItems { get; set; }

        [JsonProperty("latitude")]
        public float Latitude { get; set; }

        [JsonProperty("longitude")]
        public float Longitude { get; set; }

        [JsonProperty("schedule")]
        public string Schedule { get; set; }

        [JsonProperty("dayshours")]
        public object Dayshours { get; set; }

        [JsonProperty("approved")]
        public object Approved { get; set; }

        [JsonProperty("expirationdate")]
        public object ExpirationDate { get; set; }

        public GeoCoordinate GeoCoordinate { get; set; }
    }
}
