using Business.Exceptions;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    internal class Graph
    {
        private Dictionary<string, List<Flight>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<string, List<Flight>>();
        }

        /// <summary>
        /// Adds a flight to the graph's adjacency list.
        /// </summary>
        /// <param name="flight">The flight to be added.</param>
        public void AddFlight(Flight flight)
        {
            if (!adjacencyList.ContainsKey(flight.Origin))
            {
                adjacencyList[flight.Origin] = new List<Flight>();
            }

            adjacencyList[flight.Origin].Add(flight);
        }

        /// <summary>
        /// Retrieves the list of flights from a given origin.
        /// </summary>
        /// <param name="origin">The origin of the flights.</param>
        /// <returns>A list of flights from the specified origin.</returns>
        private List<Flight> GetFlights(string origin)
        {
            if (adjacencyList.ContainsKey(origin))
            {
                return adjacencyList[origin];
            }

            throw new FlightNotFoundException($"No flights found for the specified origin: {origin}");
        }

        /// <summary>
        /// Finds all routes for a destination from a given origin.
        /// </summary>
        /// <param name="origin">The starting point of the routes.</param>
        /// <param name="destination">The destination of the routes.</param>
        /// <returns>A list of all journeys (each represented as a Journey object) from the origin to the destination.</returns>
        public List<Journey> GetJourneysForDestination(string origin, string destination)
        {
            List<Journey> journeys = new List<Journey>();
            List<Flight> tempRoute = new List<Flight>();
            List<Flight> originFlights = GetFlights(origin);
            List<string> visitedFlightsNumber = new List<string>();

            originFlights.ForEach(flight =>
            {
                if (!visitedFlightsNumber.Contains(flight.Transport.FlightNumber))
                {
                    visitedFlightsNumber.Add(flight.Transport.FlightNumber);
                    tempRoute.Add(flight);
                    if (flight.Destination == destination)
                    {
                        journeys.Add(new Journey(new List<Flight>(tempRoute), origin, destination));

                    }
                    else
                    {
                        FindJourneys(origin, flight.Origin, flight.Destination, destination, journeys, tempRoute, visitedFlightsNumber);
                    }
                    tempRoute.Remove(flight);
                }

            });
            if (journeys.Count == 0)
            {
                throw new JourneysNotFoundException($"No journeys found for the specified origin: {origin} and destination: {destination}");
            }
            return journeys;
        }

        /// <summary>
        /// Recursive function to find all journeys for a destination from a given origin.
        /// </summary>
        /// <param name="origin">The starting point of the journeys.</param>
        /// <param name="originPreviusFlight">The origin of the previous flight in the journey.</param>
        /// <param name="actualOrigin">The current origin.</param>
        /// <param name="destination">The destination of the journeys.</param>
        /// <param name="journeys">The list of journeys found so far.</param>
        /// <param name="tempRoute">The temporary route being constructed.</param>
        /// <param name="visitedFlightsNumber">A list of flight numbers that have been visited to avoid loops.</param>
        private void FindJourneys(string origin, string originPreviousFlight, string actualOrigin, string destination, List<Journey> journeys, List<Flight> tempRoute, List<string> visitedFlightsNumber)
        {
            List<Flight> originFlights = GetFlights(actualOrigin);

            originFlights.ForEach(flight =>
            {
                if (!visitedFlightsNumber.Contains(flight.Transport.FlightNumber) && originPreviousFlight != flight.Destination && origin != flight.Destination)
                {
                    visitedFlightsNumber.Add(flight.Transport.FlightNumber);

                    tempRoute.Add(flight);

                    if (flight.Destination == destination)
                    {
                        journeys.Add(new Journey(new List<Flight>(tempRoute), origin, destination));

                    }
                    else
                    {
                        FindJourneys(origin, flight.Origin, flight.Destination, destination, journeys, tempRoute, visitedFlightsNumber);
                    }
                    tempRoute.Remove(flight);
                }

            });
        }
    }
}
