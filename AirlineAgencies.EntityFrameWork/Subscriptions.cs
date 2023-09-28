using System.ComponentModel.DataAnnotations;

namespace AirlineAgencies.EntityFrameWork
{
    public class Subscriptions
    {
        [Key]
        public int agency_id { get; set; }
        public int origin_city_id { get; set; }
        public int destination_city_id { get; set; }
    }
}
