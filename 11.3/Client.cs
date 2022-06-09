using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._3
{
    class Client
    {
        private string lastName;
        private string firstName;
        private string patronymicName;
        private string phoneNumber;
        private string idNumber;

        public Client(string lastName, string firstName, string patronymicName, string phoneNumber, string idNumber)
        {
            this.lastName = lastName;
            this.firstName = firstName;
            this.patronymicName = patronymicName;
            this.phoneNumber = phoneNumber;
            this.idNumber = idNumber;
        }

        private bool Access(byte accessLevel, string data, out string result)
        {
            //read priveledge: result or ******
            //write priveledge: true or false
            if (accessLevel == 1)
            {
                result = data;
                return false;
            }
            else if (accessLevel == 2)
            {
                result = data;
                return true;
            }
            else
            {
                result = string.Empty;
                for (int i = 0; i < data.Length; i++)
                {
                    result += "*";
                }
                return false;
            }
        }
        private void Update(string updateField)
        {
            UpdateTime = DateTime.Now;
            UpdateField = updateField;
            UpdateAccount = (IAccess.idRights == 2) ? "Manager" : "Consultant";
        }
        public string LastName
        {
            get
            {
                Access(IAccess.nameRights, lastName, out string result);
                return result;
            }
            set
            {
                if (Access(IAccess.nameRights, lastName, out string result))
                {
                    lastName = value;
                    Update("l-name");
                }
                else Console.WriteLine("В доступе отказано)");
            }
        }
        public string FirstName
        {
            get
            {
                Access(IAccess.nameRights, firstName, out string result);
                return result;
            }
            set
            {
                if (Access(IAccess.nameRights, firstName, out string result))
                {
                    firstName = value;
                    Update("f-name");
                }
                else Console.WriteLine("В доступе отказано)");
            }
        }
        public string PatronymicName
        {
            get
            {
                Access(IAccess.nameRights, patronymicName, out string result);
                return result;
            }
            set
            {
                if (Access(IAccess.nameRights, patronymicName, out string result))
                {
                    patronymicName = value;
                    Update("p-name");
                }
                else Console.WriteLine("В доступе отказано)");
            }
        }
        public string PhoneNumber
        {
            get
            {
                Access(IAccess.phoneNumberRights, phoneNumber, out string result);
                return result;
            }
            set
            {
                if (Access(IAccess.phoneNumberRights, phoneNumber, out string result))
                {
                    string newPhoneNumber = value;
                    if (!string.IsNullOrEmpty(newPhoneNumber))
                    {
                        phoneNumber = newPhoneNumber;
                        Update("phone");
                    }
                    else
                    {
                        Console.WriteLine("Поле должно быть всегда заполнено.");
                    }
                }
                else Console.WriteLine("В доступе отказано)");
            }
        }
        public string IdNumber
        {
            get
            {
                //Console.WriteLine(IAccess.idRights);
                Access(IAccess.idRights, idNumber, out string result);
                return result;
            }
            set
            {
                if (Access(IAccess.idRights, idNumber, out string result))
                {
                    idNumber = value;
                    Update("id");
                }
                else Console.WriteLine("В доступе отказано)");
            }
        }
        public DateTime UpdateTime { get; set; }
        public string UpdateField { get; set; }
        public string UpdateAccount { get; set; }

        public static void GetAccount(Client client)
        {
            Console.WriteLine("1. Фамилия: " + client.LastName + 
                "\n2. Имя: " + client.FirstName +
                "\n3. Отчество: " + client.PatronymicName +
                "\n4. Тел.: " + client.PhoneNumber + 
                "\n5. Серия и номер паспорта: " + client.IdNumber);
            if (!string.IsNullOrEmpty(client.UpdateField))
            {
                Console.WriteLine("[" + client.UpdateTime + "]" + " => " + client.UpdateField + ": " + client.UpdateAccount);
            }
            Console.WriteLine("Выберите номер записи для редактирования или введите иное чтобы вернуться: ");
            EditData(client, int.Parse(Console.ReadLine()));
        }
        private static void EditData(Client client, int number)
        {
            if (1 <= number && number <= 5)
            {
                Console.WriteLine("Введите новое значение: ");
            }
            switch (number)
            {
                case 1: client.LastName = Console.ReadLine(); break;
                case 2: client.FirstName = Console.ReadLine(); break;
                case 3: client.PatronymicName = Console.ReadLine(); break;
                case 4: client.PhoneNumber = Console.ReadLine(); break;
                case 5: client.IdNumber = Console.ReadLine(); break;
                default: Console.WriteLine("back..."); break;
            }
        }
    }
}
