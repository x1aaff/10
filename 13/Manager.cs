using System;
using System.Collections.Generic;

namespace _13
{
    class Manager
    {
        public static List<Client> GenerateClients(int number = 30)
        {
            Random random = new Random();

            List<string> names = new List<string>()
            {
                "Михаил", "Иван", "Петр", "Александр", "Семён", "Олег", "Кирилл", "Вахид"
            };

            List<Client> clients = new List<Client>();
            for (int i = 0; i < number; i++)
            {
                clients.Add(new Client(
                    names[random.Next(8)], 
                    names[random.Next(8)] + "ов", 
                    random.Next(1000000, 10000000).ToString())
                    );
                clients[i].Open<DepositAccount>();
                clients[i].ClientAccounts[0].Worth = random.Next(100000);
                clients[i].Open<AnotherAccount>();
                clients[i].ClientAccounts[1].Worth = random.Next(10000);
            }

            return clients;
        }
        public static List<Client> clientsList = GenerateClients();
    }
}
