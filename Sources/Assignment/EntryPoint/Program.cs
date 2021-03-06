﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntryPoint
{
#if WINDOWS || LINUX
    public static class Program
    {
        static void CrashMe(int n)
        {
            Console.Write("Going strong at level " + n + "\r");
            CrashMe(n + 1);
            //WHY THIS FUNCTION?!?!
        }

        [STAThread]
        static void Main()
        {
            //CrashMe(0);

            var fullscreen = false;
            read_input:
            switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, 3, 4, or q for quit)", "Choose assignment", VirtualCity.GetInitialValue()))
            {
                case "1":
                    using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance, fullscreen))
                        game.Run();
                    break;
                case "2":
                    using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse, fullscreen))
                        game.Run();
                    break;
                case "3":
                    using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
                        game.Run();
                    break;
                case "4":
                    using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
                        game.Run();
                    break;
                case "q":
                    return;
            }
            goto read_input;
        }

        private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings)
        {
            return MergeSort.Sort(specialBuildings, (A,B) => Vector2.Distance(A, house) <= Vector2.Distance(B, house));
            //return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
        }

        private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
          IEnumerable<Vector2> specialBuildings,
          IEnumerable<Tuple<Vector2, float>> housesAndDistances)
        {
            kdTree tree = new kdTree(specialBuildings);
            
            List<List<Vector2>> ListOfLists = new List<List<Vector2>>();

            foreach(var i in housesAndDistances)
            {
                ListOfLists.Add(tree.ReturnWhithinRange(i.Item1, i.Item2));
            }
            return ListOfLists;
            //return
            //    from h in housesAndDistances
            //    select
            //      from s in specialBuildings
            //      where Vector2.Distance(h.Item1, s) <= h.Item2
            //      select s;
        }

        private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding,
          Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
            List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
            var prevRoad = startingRoad;
            for (int i = 0; i < 30; i++)
            {
                prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, destinationBuilding)).First());
                fakeBestPath.Add(prevRoad);
            }
            return fakeBestPath;
        }



        private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding,
          IEnumerable<Vector2> destinationBuildings, IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            FloydWarshall FloydDistances = new FloydWarshall();

            //temp
            /*
            var testdata = new List<Tuple<Vector2, Vector2>>();
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(0, 0), new Vector2(1, 0)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(1, 0), new Vector2(2, 0)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(2, 0), new Vector2(3, 0)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(3, 0), new Vector2(4, 0)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(4, 0), new Vector2(4, 1)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(4, 1), new Vector2(4, 2)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(3, 2), new Vector2(4, 2)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(3, 2), new Vector2(2, 2)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(2, 2), new Vector2(2, 1)));
            testdata.Add(new Tuple<Vector2, Vector2>(new Vector2(2, 1), new Vector2(2, 0)));

            roads = testdata;*/
            //end temp

            foreach (var i in roads)
            {
                FloydDistances.AddConnection(i.Item1, i.Item2);
            }
            FloydDistances.CalculateDistances();
            
            List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();

            foreach (var i in destinationBuildings)
            {
                result.Add( FloydDistances.GetShortestPath(startingBuilding, i));
            }
            //FloydDistances.GetShortestPath(new Vector2(0,0), new Vector2(4, 2));

            /*
            foreach (var d in destinationBuildings)
            {
                var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
                List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
                var prevRoad = startingRoad;
                for (int i = 0; i < 30; i++)
                {
                    prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, d)).First());
                    fakeBestPath.Add(prevRoad);
                }
                result.Add(fakeBestPath);
            }*/
            return result;
        }



    }
#endif
}
