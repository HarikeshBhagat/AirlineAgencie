using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineAgencies.EntityFrameWork
{
    public class Subscriptions
    {
        [Key]
        public Int64 agency_id { get; set; }
        public Int64 origin_city_id { get; set; }
        public Int64 destination_city_id { get; set; }
    }
}
