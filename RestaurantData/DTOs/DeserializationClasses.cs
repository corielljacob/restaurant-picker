using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantData.DTOs
{
    public class AddressObj
    {
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postalcode { get; set; }
    }

    public class Ancestor
    {
        public List<Subcategory> subcategory { get; set; }
        public string name { get; set; }
        public string abbrv { get; set; }
        public string location_id { get; set; }
    }

    public class Category
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class Cuisine
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class MongoDatum
    {
        [BsonElement("_id")]
        public ObjectId? ObjectId { get; set; }
        public string location_id { get; set; }
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string num_reviews { get; set; }
        public string timezone { get; set; }
        public string location_string { get; set; }
        public Photo photo { get; set; }
        public string api_detail_url { get; set; }
        public List<Award> awards { get; set; }
        public string doubleclick_zone { get; set; }
        public string preferred_map_engine { get; set; }
        public string raw_ranking { get; set; }
        public string ranking_geo { get; set; }
        public string ranking_geo_id { get; set; }
        public string ranking_position { get; set; }
        public string ranking_denominator { get; set; }
        public string ranking_category { get; set; }
        public string ranking { get; set; }
        public int? distance { get; set; }
        public string distance_string { get; set; }
        public string bearing { get; set; }
        public string rating { get; set; }
        public bool is_closed { get; set; }
        public string open_now_text { get; set; }
        public bool is_long_closed { get; set; }
        public string price_level { get; set; }
        public string description { get; set; }
        public string web_url { get; set; }
        public string write_review { get; set; }
        public List<Ancestor> ancestors { get; set; }
        public Category category { get; set; }
        public List<Subcategory> subcategory { get; set; }
        public string parent_display_name { get; set; }
        public bool is_jfy_enabled { get; set; }
        public List<NearestMetroStation> nearest_metro_station { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public AddressObj address_obj { get; set; }
        public string address { get; set; }
        public Hours hours { get; set; }
        public bool is_candidate_for_contact_info_suppression { get; set; }
        public List<Cuisine> cuisine { get; set; }
        public List<DietaryRestriction> dietary_restrictions { get; set; }
        public List<EstablishmentType> establishment_types { get; set; }
    }

    public class Datum
    {
        public string location_id { get; set; }
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string num_reviews { get; set; }
        public string timezone { get; set; }
        public string location_string { get; set; }
        public Photo photo { get; set; }
        public string api_detail_url { get; set; }
        public List<Award> awards { get; set; }
        public string doubleclick_zone { get; set; }
        public string preferred_map_engine { get; set; }
        public string raw_ranking { get; set; }
        public string ranking_geo { get; set; }
        public string ranking_geo_id { get; set; }
        public string ranking_position { get; set; }
        public string ranking_denominator { get; set; }
        public string ranking_category { get; set; }
        public string ranking { get; set; }
        public int? distance { get; set; }
        public string distance_string { get; set; }
        public string bearing { get; set; }
        public string rating { get; set; }
        public bool is_closed { get; set; }
        public string open_now_text { get; set; }
        public bool is_long_closed { get; set; }
        public string price_level { get; set; }
        public string description { get; set; }
        public string web_url { get; set; }
        public string write_review { get; set; }
        public List<Ancestor> ancestors { get; set; }
        public Category category { get; set; }
        public List<Subcategory> subcategory { get; set; }
        public string parent_display_name { get; set; }
        public bool is_jfy_enabled { get; set; }
        public List<NearestMetroStation> nearest_metro_station { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public AddressObj address_obj { get; set; }
        public string address { get; set; }
        public Hours hours { get; set; }
        public bool is_candidate_for_contact_info_suppression { get; set; }
        public List<Cuisine> cuisine { get; set; }
        public List<DietaryRestriction> dietary_restrictions { get; set; }
        public List<EstablishmentType> establishment_types { get; set; }
    }

    public class Award
    {

    }

    public class NearestMetroStation
    {

    }

    public class DietaryRestriction
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class EstablishmentType
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class Hours
    {
        public List<List<OpenCloseTime>> week_ranges { get; set; }
        public string timezone { get; set; }
    }

    public class OpenCloseTime
    {
        public int Open_Time { get; set; }
        public int Close_Time { get; set; }
    }

    public class Images
    {
        public Small small { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Original original { get; set; }
        public Large large { get; set; }
        public Medium medium { get; set; }
    }

    public class Large
    {
        public string width { get; set; }
        public string url { get; set; }
        public string height { get; set; }
    }

    public class Medium
    {
        public string width { get; set; }
        public string url { get; set; }
        public string height { get; set; }
    }

    public class OpenHoursOptions
    {
        public string closed_count { get; set; }
        public bool is_set { get; set; }
        public string timezone { get; set; }
        public string unsure_count { get; set; }
        public string open_count { get; set; }
        public string current_value { get; set; }
    }

    public class Option
    {
        public string value { get; set; }
        public string display { get; set; }
        public bool? selected { get; set; }
    }

    public class Original
    {
        public string width { get; set; }
        public string url { get; set; }
        public string height { get; set; }
    }

    public class Paging
    {
        public string previous { get; set; }
        public string next { get; set; }
        public string skipped { get; set; }
        public string results { get; set; }
        public string total_results { get; set; }
    }

    public class PeopleOptions
    {
        public SelectedOption selected_option { get; set; }
        public List<Option> options { get; set; }
    }

    public class Photo
    {
        public Images images { get; set; }
        public bool is_blessed { get; set; }
        public DateTime uploaded_date { get; set; }
        public string caption { get; set; }
        public string id { get; set; }
        public string helpful_votes { get; set; }
        public DateTime published_date { get; set; }
        public User user { get; set; }
    }

    public class RestaurantAvailabilityOptions
    {
        public string day { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string hour { get; set; }
        public string minute { get; set; }
        public string people { get; set; }
        public string datestring { get; set; }
        public bool is_default { get; set; }
        public bool is_set { get; set; }
        public bool racable { get; set; }
        public TimeOptions time_options { get; set; }
        public PeopleOptions people_options { get; set; }
    }

    public class Results
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
        public RestaurantAvailabilityOptions restaurant_availability_options { get; set; }
        public OpenHoursOptions open_hours_options { get; set; }
    }

    public class Root
    {
        public int status { get; set; }
        public object msg { get; set; }
        public Results results { get; set; }
    }

    public class SelectedOption
    {
        public string value { get; set; }
        public string display { get; set; }
        public bool selected { get; set; }
    }

    public class Small
    {
        public string width { get; set; }
        public string url { get; set; }
        public string height { get; set; }
    }

    public class Subcategory
    {
        public string key { get; set; }
        public string name { get; set; }
    }

    public class Thumbnail
    {
        public string width { get; set; }
        public string url { get; set; }
        public string height { get; set; }
    }

    public class TimeOptions
    {
        public SelectedOption selected_option { get; set; }
        public List<Option> options { get; set; }
    }

    public class User
    {
        public string user_id { get; set; }
        public string member_id { get; set; }
        public string type { get; set; }
    }


}
