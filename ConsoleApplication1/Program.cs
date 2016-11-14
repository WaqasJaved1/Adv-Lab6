using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {


        public static int pcAssemblyRec(int[,] time, int[,] change, int[] entry, int[] exit, int n, int line)
        {
            if (n == 0)
            {
                return entry[line] + time[line,0];
            }

            int T0 = 10000;
            int T1 = 10000;
            int T2 = 10000;
            if (line == 0)
            {
                T0 = min(pcAssemblyRec(time, change, entry, exit, n - 1, 0) + time[0,n],
                                    pcAssemblyRec(time, change, entry, exit, n - 1, 1) + change[2, n] + time[0, n],
                                    pcAssemblyRec(time, change, entry, exit, n - 1, 2) + change[4, n] + time[0, n]);
            }
            else if (line == 1)
            {
                T1 = min(pcAssemblyRec(time, change, entry, exit, n - 1, 1) + time[1, n],
                                    pcAssemblyRec(time, change, entry, exit, n - 1, 0) + change[0, n] + time[1, n],
                                    pcAssemblyRec(time, change, entry, exit, n - 1, 2) + change[5, n] + time[1, n]);
            }
            else if (line == 2)
            {
                T2 = min(pcAssemblyRec(time, change, entry, exit, n - 1, 2) + time[2, n],
                                    pcAssemblyRec(time, change, entry, exit, n - 1, 0) + change[1, n] + time[2, n],
                                    pcAssemblyRec(time, change, entry, exit, n - 1, 1) + change[3, n] + time[2, n]);
            }

            return min(T0, T1, T2);
        }

        public static int recursive_scheduling(int[,] time, int[,] change, int[] entry, int[] exit) { 
            int x = pcAssemblyRec(time, change, entry, exit, 4, 0);
            int y = pcAssemblyRec(time, change, entry, exit, 4, 1);
            int z = pcAssemblyRec(time, change, entry, exit, 4, 2);
            
            return min(x+exit[0],y+exit[1],z+exit[2]);
        }

        static void print(int[,] time, int[,] change, int[] entry, int[] exit) {
            Console.WriteLine("Total Lines: 3");
            Console.WriteLine("Total Stations: 5");
            Console.WriteLine("Prcessing Time for station of Line: 1");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(time[0,i] + "\t");
            }
            Console.WriteLine("\nPrcessing Time for station of Line: 2");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(time[1, i] + "\t");
            }
            Console.WriteLine("\nPrcessing Time for station of Line: 2");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(time[2, i] + "\t");
            }

            Console.WriteLine("\nEntry Times");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(entry[i] + "\t");
            }

            Console.WriteLine("\nExit Times");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(exit[i] + "\t");
            }

            Console.WriteLine("Change lane time: 1 details given in comments");

        }
        static int min(int a, int b, int c) { 
            if(a <= b && a<= c){
                return a;
            }
            else if (b <= a && b <= c)
            {
                return b;
            }

            return c;
        
        }

        static int iterative_scheduling(int[,] time, int[,] change, int[] entry, int[] exit) {

            int[,] T = new int[3, 5];

            for(int i = 0; i< 3;i++){
                T[i,0] = entry[i] + time[i,0];
            }

            for (int i = 1; i < 5; ++i)
            {

                //select min of current station (previous lane 1st,previous lane 2nd,previous lane 3rd)
                T[0, i] = min(T[0, i - 1] + time[0, i], T[1, i - 1] + change[2, i] + time[0, i], T[2, i - 1] + change[4, i] + time[0, i]);
                //select min of current station (previous lane 1st,previous lane 2nd,previous lane 3rd)                
                T[1, i] = min(T[1, i - 1] + time[1, i], T[0, i - 1] + change[0, i] + time[1, i], T[2, i - 1] + change[5, i] + time[1, i]);
                //select min of current station (previous lane 1st,previous lane 2nd,previous lane 3rd)                
                T[2, i] = min(T[2, i - 1] + time[2, i], T[1, i - 1] + change[1, i] + time[2, i], T[2, i - 1] + change[3, i] + time[2, i]);
            }


            return min(T[0, 4] + exit[0], T[1, 4] + exit[1], T[2, 4] + exit[2]);
        }
        static void Main(string[] args)
        {
            //declaring variable

            //time[i,j] represent time taken by j station in i lane
            int[,] time = new int[3, 5];
            //change[0,j] represent Shift from 1 -> 2 lane of j station
            //change[1,j] represent Shift from 1 -> 3 lane of j station
            //change[2,j] represent Shift from 2 -> 1 lane of j station
            //change[3,j] represent Shift from 2 -> 3 lane of j station
            //change[4,j] represent Shift from 3 -> 1 lane of j station
            //change[5,j] represent Shift from 3 -> 2 lane of j station
            int[,] change = new int[6,5];

            //entry[i] represent i lane entry time
            int[] entry = new int[3];

            //exit[i] represent i lane exit time
            int[] exit = new int[3];

            //initializing variable with random value

            var rand = new Random();
            for (int i = 0; i < 3;i++)
            {
                for (int j = 0; j < 5;j++ )
                {
                    time[i, j] = rand.Next(10, 50) + rand.Next(2, 6)*j;
                }
                entry[i] =rand.Next(5,10);
                exit[i] = rand.Next(5, 10);
            }

            //initializing change lane time with random value
            for (int i = 0; i < 6; i++ )
            {
                for (int j = 0; j < 5; j++)
                {
                    change[i, j] = rand.Next(5, 20);
                }
            }

            print(time, change, entry, exit);
            int Min_time_iterative = iterative_scheduling(time,change,entry,exit);

            int Min_time_recursive = recursive_scheduling(time, change, entry, exit);
            Console.WriteLine("\nTime With Iterative Solution:" + Min_time_iterative+ "\nTime with Recursive Solution" + Min_time_recursive);

            Console.ReadLine();

        }
    }
}
