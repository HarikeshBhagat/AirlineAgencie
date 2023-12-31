﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineAgencies.EntityFrameWork
{
    public class Routes
    {
        [Key]
        public Int64 route_id { get; set; }
        public Int64 origin_city_id { get; set; }
        public Int64 destination_city_id { get; set; }
        public DateTime departure_date { get; set; }
    }
}
