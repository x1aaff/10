using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace _11._2
{
    public class Manager : Consultant
    {
        private static bool logged = false;
        public static bool Logged { get { return logged; } }
        public static void ActivateLogin()
        {
            logged = true;
        }
        public static void AddClient()
        {
            string lastName, firstName, patronymicName, phoneNumber, idNumber;
            Console.WriteLine("Фамилия: ");
            lastName = Console.ReadLine();
            Console.WriteLine("Имя: ");
            firstName = Console.ReadLine();
            Console.WriteLine("Отчество: ");
            patronymicName = Console.ReadLine();
            Console.WriteLine("Тел.: ");
            phoneNumber = Console.ReadLine();
            Console.WriteLine("Серия, номер паспорта: ");
            idNumber = Console.ReadLine();
            Program.clients.Add(new Client(lastName, firstName, patronymicName, phoneNumber, idNumber));

            /*string jsonString = JsonSerializer.Serialize(Program.clients);
            File.WriteAllText("clients.json", jsonString);*/
            Console.WriteLine("Записано!");
        }
    }
}
