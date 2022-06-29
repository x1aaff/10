using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace _16
{
    class Program
    {
        static Random random = new Random();

        static async Task Main(string[] args)
        {
            
            Flag.Arouse();

            var townhallTask = Townhall.RepairTownhall();
            var schoolTask = School.ImproveSchool();
            var roadTask = Road.Make();

            var cityTasks = new List<Task> { townhallTask, schoolTask, roadTask };
            while (cityTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(cityTasks);
                cityTasks.Remove(finishedTask);
            }

            Flag.ArouseSecond();
        }

        class Flag
        {
            public static void Arouse() => Console.WriteLine("Settlement Flag has been Aroused.");
            public static void ArouseSecond() => Console.WriteLine("Beautiful Settlement Flag has been Aroused!");
        }
        class Townhall
        {
            class Pillar { }
            class Walls { }
            static async Task<Pillar> BuildPillars(int count)
            {
                for (int pillar = 0; pillar < count; pillar++)
                {
                    Console.WriteLine($"workers are installing Pillar #{pillar} for [Townhall]...");
                    await Task.Delay(random.Next(800, 2000));
                    Console.WriteLine($"[Townhall] Pillar #{pillar} has been erected.");
                }
                return new Pillar();
            }
            static async Task<Walls> GildWalls()
            {
                Console.WriteLine("workers are gilding walls of [Townhall]...");
                await Task.Delay(random.Next(2000, 4000));
                Console.WriteLine("[Townhall] walls are Golden.");
                return new Walls();
            }

            public static async Task<Townhall> RepairTownhall()
            {
                var wallsTask = GildWalls();
                var pillarTask = BuildPillars(4);

                await Task.WhenAll(wallsTask, pillarTask);
                return new Townhall();
            }
        }
        class School
        {
            class Desk { }
            class Personal { }
            static async Task<Desk> MakeDesks()
            {
                Console.WriteLine("workers are making Desks for [School]...");
                await Task.Delay(random.Next(2800, 4200));
                Console.WriteLine("New [School] Desks done.");
                return new Desk();
            }
            static async Task<Personal> EducatePsychologist()
            {
                Console.WriteLine("there is new [School] Psychologist...");
                await Task.Delay(random.Next(500, 1500));
                Console.WriteLine("[School] Psychologist is now competent to: Solve Conflicts.");
                await Task.Delay(random.Next(500, 1500));
                Console.WriteLine("[School] Psychologist is now competent to: Improve Relationship.");
                await Task.Delay(random.Next(500, 1500));
                Console.WriteLine("[School] Psychologist is now competent to: Eat Donuts.");
                return new Personal();
            }

            public static async Task<School> ImproveSchool()
            {
                var deskTask = MakeDesks();
                var personalTask = EducatePsychologist();

                await Task.WhenAll(deskTask, personalTask);
                return new School();
            }
        }
        class Road
        {
            public static async Task<Road> Make()
            {
                Console.WriteLine("workers are busy with new [Road]...");
                await Task.Delay(500);
                Console.WriteLine("Asphalt dries and workers just watching near the [Road]...");
                await Task.Delay(2200);
                Console.WriteLine("New [Road] has been paved.");
                return new Road();
            }
        }
    }
}
