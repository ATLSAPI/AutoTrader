using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    namespace AutoService
    {
        public class AdditionalInfo
        {
            public string featuredDealerPosition { get; set; }
            public string featuredListingPosition { get; set; }
        }

        public class HookLogicBanner
        {
            public string url { get; set; }
            public AdditionalInfo additionalInfo { get; set; }
        }

        public class Banners
        {
            public HookLogicBanner hookLogicBanner { get; set; }
        }

        public class Region
        {
            public int numberOfAdverts { get; set; }
            public string name { get; set; }
            public bool isFake { get; set; }
        }

        public class County
        {
            public Region region { get; set; }
            public int numberOfAdverts { get; set; }
            public string name { get; set; }
            public bool isFake { get; set; }
        }

        public class Town
        {
            public County county { get; set; }
            public int numberOfAdverts { get; set; }
            public string name { get; set; }
            public bool isFake { get; set; }
        }

        public class AdvertAttributes
        {
            public string advertTitle { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public int advertVersionNumber { get; set; }
            public object advertPlacementDate { get; set; }
            public string advertPlacementDateFormatted { get; set; }
            public int daysOld { get; set; }
            public bool isBasicAdvert { get; set; }
            public bool displayAdvertiserAddress { get; set; }
            public bool isTrade { get; set; }
            public bool isNearlyNew { get; set; }
            public bool isUsedApproved { get; set; }
            public bool partExchangeAllowed { get; set; }
            public int price { get; set; }
            public string priceFormatted { get; set; }
            public string vehiclePriceSaving { get; set; }
            public bool telesafe { get; set; }
            public string badAdType { get; set; }
            public string oneSearchAds { get; set; }
            public bool displaySlideShow { get; set; }
            public string videoId { get; set; }
            public bool videoAudioDisabled { get; set; }
            public bool premiumProduct { get; set; }
            public int owners { get; set; }
            public string advertExpiryDate { get; set; }
            public List<double?> vehicleLatLong { get; set; }
            public Town town { get; set; }
            public string attentionGrabber { get; set; }
        }

        public class PhoneNumber1
        {
            public string formattedPhoneNumber { get; set; }
            public bool shouldAddAsteriskIfIsCallTrackerNumber { get; set; }
        }

        public class PhoneNumber2
        {
            public string formattedPhoneNumber { get; set; }
            public bool shouldAddAsteriskIfIsCallTrackerNumber { get; set; }
        }

        public class AdvertiserAttributes
        {
            public PhoneNumber1 phoneNumber1 { get; set; }
            public PhoneNumber2 phoneNumber2 { get; set; }
            public string email { get; set; }
            public string emailLink { get; set; }
        }

        public class VehicleAttributes
        {
            public string make { get; set; }
            public string model { get; set; }
            public string vrm { get; set; }
            public string vehicleRegistrationDate { get; set; }
            public int quickQuoteInsuranceGroup { get; set; }
            public string vehicleBerth { get; set; }
            public string vehicleMileageUnit { get; set; }
            public int totalUserReviews { get; set; }
            public bool vehicleCheck { get; set; }
            public string yearOfManufacture { get; set; }
            public string regLetter { get; set; }
            public string age { get; set; }
            public string transmission { get; set; }
            public string fuelType { get; set; }
            public string bodyType { get; set; }
            public string mileage { get; set; }
            public string mileageFormatted { get; set; }
            public string engineSize { get; set; }
            public string colour { get; set; }
            public string numDoors { get; set; }
            public string mrp { get; set; }
            public int? userReviewRating { get; set; }
            public string v5 { get; set; }
            public int? co2Emissions { get; set; }
            public string atDerivativeId { get; set; }
            public double? annualTax { get; set; }
        }

        public class Region2
        {
            public int numberOfAdverts { get; set; }
            public string name { get; set; }
            public bool isFake { get; set; }
        }

        public class County2
        {
            public Region2 region { get; set; }
            public int numberOfAdverts { get; set; }
            public string name { get; set; }
            public bool isFake { get; set; }
        }

        public class StructuredLocation
        {
            public County2 county { get; set; }
            public int numberOfAdverts { get; set; }
            public string name { get; set; }
            public bool isFake { get; set; }
        }

        public class DealerAttributes
        {
            public string id { get; set; }
            public string oldDealerStockFlag { get; set; }
            public int stockCount { get; set; }
            public List<string> products { get; set; }
            public List<object> additionalLinks { get; set; }
            public List<object> promotionBullets { get; set; }
            public List<object> affiliates { get; set; }
            public List<object> newCarMakes { get; set; }
            public List<object> usedMakes { get; set; }
            public List<object> newBikeMakes { get; set; }
            public List<object> usedBikeMakes { get; set; }
            public bool addressDisplayable { get; set; }
            public List<object> franchises { get; set; }
            public StructuredLocation structuredLocation { get; set; }
            public string numberOfReviews { get; set; }
        }

        public class ClassifiedAdvert
        {
            public string id { get; set; }
            public string channel { get; set; }
            public int distanceFromVehicleLocation { get; set; }
            public string searchTarget { get; set; }
            public AdvertAttributes advertAttributes { get; set; }
            public AdvertiserAttributes advertiserAttributes { get; set; }
            public VehicleAttributes vehicleAttributes { get; set; }
            public string capTechSpecFlag { get; set; }
            public string techSpecFlag { get; set; }
            public DealerAttributes dealerAttributes { get; set; }
            public string atDerivativeId { get; set; }
        }

        public class Option
        {
            public string name { get; set; }
            public int count { get; set; }


        }


        public class Refinement
        {
            public string name { get; set; }
            public bool multiSelect { get; set; }
            public List<Option> options { get; set; }
        }

        public class SearchResults
        {
            public List<ClassifiedAdvert> classifiedAdverts { get; set; }

            public int totalResults { get; set; }
        }



        public class PageSet
        {
            public string displayName { get; set; }
            public string value { get; set; }
            public bool selected { get; set; }
            public string link { get; set; }
        }

        public class Paginator
        {
            public int currentPage { get; set; }
            public int totalResults { get; set; }
            public List<PageSet> pageSet { get; set; }
            public int resultsPerPage { get; set; }
        }

        public class SortOrderInfo
        {
            public string Sort_Key { get; set; }
            public string Sort_Order { get; set; }
        }

        public class RequestInfo
        {
            public string basepath { get; set; }
            public string imagebasepath { get; set; }
            public string editorialimagbasepath { get; set; }
            public string requesturl { get; set; }
        }

        public class AutoTraderJSON_CsharpClass
        {

            /*public List<string> searchRoutes { get; set; }
           public Banners banners { get; set; }
           public SearchResults searchResults { get; set; }
           public Paginator paginator { get; set; }
           public List<SortOrderInfo> sortOrderInfo { get; set; }
           public List<object> refinements { get; set; }
           public RequestInfo requestInfo { get; set; }*/

            public List<string> searchRoutes { get; set; }
            // public List<Link> links { get; set; }
            public SearchResults searchResults { get; set; }
            public Paginator paginator { get; set; }
            // public int totalResults { get; set; }
            public Banners banners { get; set; }
            public List<Refinement> refinements { get; set; }
            public List<SortOrderInfo> sortOrderInfo { get; set; }
            public RequestInfo requestInfo { get; set; }

        }
    }
}