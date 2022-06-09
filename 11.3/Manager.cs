using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._3
{
    public class Manager : IAccess
    {
        private static bool logged = false;
        public static bool Logged { get { return logged; } }
        void IAccess.Login()
        {
            IAccess.nameRights = 2;
            IAccess.phoneNumberRights = 2;
            IAccess.idRights = 2;
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

            Console.WriteLine("Записано!");
        }
    }
}
