using AirlineAgencies.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AirlineAgencies
{
    public class DetectionAlgorithm
    {
        private AirlineAgenciesDbContext dbContext = new AirlineAgenciesDbContext();
        private DateTime currentDate = DateTime.Now;

        public void ChangeDetectionAlgorithm(DateTime startDate, DateTime endDate, Int64 agencyId)
        {
            DateTime startTime = DateTime.Now;
            #region DetectNewFlights
            var newFlights = CheckNewFlights(startDate, endDate, agencyId);
            #endregion

            #region DetectDiscontinuedFlights
            var discontinuedFlights = CheckDiscontinuedFlights(startDate, endDate, agencyId);
            #endregion DetectNewFlights

            WriteToCSV(newFlights, discontinuedFlights);

            DateTime endTime = DateTime.Now;
            TimeSpan executionTime = endTime - startTime;
            Console.WriteLine($"Execution time: {executionTime.TotalMilliseconds} ms");

        }
        private List<ResultViewModel> CheckNewFlights(DateTime startDate, DateTime endDate, Int64 agencyId)
        {
            Console.WriteLine("Start : Check New Flights.");

            DateTime previousWeek = startDate.AddDays(-7).AddMinutes(-30);
            DateTime nextWeek = endDate.AddMinutes(30);

            var newFlights = dbContext.Subscriptions.Where(x => x.agency_id == agencyId)
                                      .Join(
                                      dbContext.Routes,
                                      Subscriptions => Subscriptions.origin_city_id,
                                      Routes => Routes.origin_city_id,
                                      (Subscriptions, Routes) => new
                                      {
                                          Subscriptions = Subscriptions,
                                          Routes = Routes,
                                      }).
                                      Join(dbContext.Flights
                                      .Where(f => f.departure_time >= previousWeek && f.departure_time <= nextWeek),
                                      joinResult => joinResult.Routes.route_id,
                                      Flights => Flights.route_id,
                                                    (joinResult, Flights) => new ResultViewModel
                                                    {
                                                        airline_id = Flights.airline_id,
                                                        flight_id = Flights.flight_id,
                                                        origin_city_id = joinResult.Routes.origin_city_id,
                                                        destination_city_id = joinResult.Routes.destination_city_id,
                                                        departure_time = Flights.departure_time,
                                                        arrival_time = Flights.arrival_time
                                                    }).Distinct().ToList();

            Console.WriteLine("End : Check New Flights.");

            return newFlights;
        }
        private List<ResultViewModel> CheckDiscontinuedFlights(DateTime startDate, DateTime endDate, Int64 agencyId)
        {
            Console.WriteLine("Start : Check Discontinued Flights.");
            DateTime previousWeek = startDate.AddMinutes(-30);
            DateTime nextWeek = endDate.AddDays(7).AddMinutes(30);

            var discontinuedFlights = dbContext.Subscriptions.Where(x => x.agency_id == agencyId)
                                      .Join(
                                      dbContext.Routes,
                                      Subscriptions => Subscriptions.origin_city_id,
                                      Routes => Routes.origin_city_id,
                                      (Subscriptions, Routes) => new
                                      {
                                          Subscriptions = Subscriptions,
                                          Routes = Routes,
                                      }).
                                      Join(dbContext.Flights
                                      .Where(f => f.departure_time >= previousWeek && f.departure_time <= nextWeek),
                                      joinResult => joinResult.Routes.route_id,
                                      Flights => Flights.route_id,
                                                    (joinResult, Flights) => new ResultViewModel
                                                    {
                                                        airline_id = Flights.airline_id,
                                                        flight_id = Flights.flight_id,
                                                        origin_city_id = joinResult.Routes.origin_city_id,
                                                        destination_city_id = joinResult.Routes.destination_city_id,
                                                        departure_time = Flights.departure_time,
                                                        arrival_time = Flights.arrival_time
                                                    }).Distinct().ToList();

            Console.WriteLine("End : Check Discontinued Flights.");

            return discontinuedFlights;
        }
        private void WriteToCSV(List<ResultViewModel> newFlights, List<ResultViewModel> discontinuedFlights)
        {
            using (StreamWriter writer = new StreamWriter("results.csv"))
            {

                Console.WriteLine("Start : Adding result into results.csv.");

                writer.WriteLine("flight_id,origin_city_id,destination_city_id,departure_time,arrival_time,airline_id,status");

                foreach (var flight in newFlights)
                {
                    if (flight.arrival_time != Convert.ToDateTime("31-12-9999  23:59:00"))
                    {

                        writer.WriteLine($"{flight.flight_id},{flight.origin_city_id},{flight.destination_city_id},{flight.departure_time},{flight.arrival_time.AddHours(2)},{flight.airline_id},New");
                    }
                }

                foreach (var flight in discontinuedFlights)
                {
                    if (flight.arrival_time != Convert.ToDateTime("31-12-9999  23:59:00"))
                    {
                        writer.WriteLine($"{flight.flight_id},{flight.origin_city_id},{flight.destination_city_id},{flight.departure_time},{flight.arrival_time.AddHours(2)},{flight.airline_id},Discontinued");
                    }

                }

                Console.WriteLine("End : Adding result into results.csv.");
                Console.WriteLine("Airlines data added to results.csv");
            }
        }
    }
}
