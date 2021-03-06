﻿using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using FourLeggedHead.Tools;

namespace DayThirteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Thirteen");

            var path = @"Resources/Input.txt";

            try
            {
                var input = FileReader.ReadAllLines(path);

                var timestamp = int.Parse(input.ElementAt(0));
                var busIds = input.ElementAt(1).Split(',').Where(b => b != "x").Select(b => int.Parse(b)).ToList();
                var waits = busIds.Select(b => b * (int)((timestamp / b) + 1) - timestamp).ToList();
                var smalestWait = waits.Min();

                Console.WriteLine(busIds[waits.IndexOf(smalestWait)] * smalestWait);

                var listOfBuses = input.ElementAt(1).Split(',');
                var inServiceBusCount = listOfBuses.Count(b => b != "x");

                var inServiceBuses = new int[inServiceBusCount];
                var busWaits = new int[inServiceBusCount];

                var busId = 0;
                for (int i = 0; i < listOfBuses.Length; i++)
                {
                    if (listOfBuses[i] != "x")
                    {
                        inServiceBuses[busId] = int.Parse(listOfBuses[i]);
                        busWaits[busId] = i;
                        busId++;
                    }
                }

                var maxWait = busWaits.Last();
                busWaits = busWaits.Select(w => maxWait - w).ToArray();

                Console.WriteLine(CRT.findMinX(inServiceBuses, busWaits, inServiceBusCount) - maxWait);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
