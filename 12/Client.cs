using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace _12
{
    class Client
    {
        public static IAccess access;
        public static List<Client> clients = new List<Client>();
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
            File.WriteAllText("clients.json", jsonString);
        }
    }
}
