﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineAgencies.EntityFrameWork
{
    public class Flights
    {
        [Key]
        public Int64 flight_id { get; set; }
        public Int64 route_id { get; set; }
        public DateTime departure_time { get; set; }
        public DateTime arrival_time { get; set; }
        public Int64 airline_id { get; set; }
    }
}
