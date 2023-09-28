using System;

namespace AirlineAgencies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Please input valid parameters.");
                return;
            }

            if (!DateTime.TryParse(args[0], out DateTime startDate) ||
                !DateTime.TryParse(args[1], out DateTime endDate) ||
                !int.TryParse(args[2], out int agencyId))
            {
                Console.WriteLine("Invalid input parameters.");
                return;
            }

            Console.WriteLine("Start : Airline system change algorithm.");

            DetectionAlgorithm AirlinesAlgorithm = new DetectionAlgorithm();
            AirlinesAlgorithm.ChangeDetectionAlgorithm(startDate, endDate, agencyId);

            Console.WriteLine("End : Airline system change algorithm.");

            Console.WriteLine("Press anykey to generate airline data.");
            Console.ReadKey();
        }
    }
}
