using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;


namespace _11
{
    class Program
    {
        static public void GenerateData(int count)
        {
            Random random = new Random();
            List<string> names = new List<string>()
            {
                "Вахид", "Джабир", "Искандер", "Мазхар", "Намир", "Рашид", "Рияд", "Фазиль", "Аль", "Хани"
            };

            List<Client> clients = new List<Client>();
            for (int i = 0; i < count; i++)
            {
                clients.Add(new Client(names[random.Next(10)], names[random.Next(10)], names[random.Next(10)],
                    random.Next(100000, 1000000).ToString(), random.Next(100000, 1000000).ToString()));
            }

            string jsonString = JsonSerializer.Serialize(clients);
            //if (!File.Exists("clients.json")) File.Create("clients.json").Close();
            File.WriteAllText("clients.json", jsonString);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Сгенерировать данные? 1 - да, остальное - нет:");
            if (Console.ReadLine() == "1")
            {
                GenerateData(10);
            }
            if (!File.Exists("clients.json")) File.Create("clients.json").Close();
            List<Client> clients = new List<Client>();
            clients = JsonSerializer.Deserialize<List<Client>>(File.ReadAllText("clients.json"));
            
            for (int i = 0; i < clients.Count; i++)
            {
                Console.WriteLine(i + 1 + ". id" + i);
            }
            Console.WriteLine("Выбрать запись для просмотра: ");
            Client.GetAccount(clients[int.Parse(Console.ReadLine()) - 1]);

            string jsonString = JsonSerializer.Serialize(clients);
            File.WriteAllText("clients.json", jsonString);
        }
    }
}
