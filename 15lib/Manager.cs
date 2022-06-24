using System;
using System.Collections.Generic;

namespace _15lib
{
    public delegate void NotifyDelegate(string msg);
    
    static class Ext
    {
        static Random random2 = new Random();
        public static void FillDepositAcc(this Client client)
        {
            client.ClientAccounts[0].Worth = random2.Next(100000);
        }
        public static void FillAnotherAcc(this Client client)
        {
            client.ClientAccounts[1].Worth = random2.Next(10000);
        }
    }
    public class Manager
    {
        static int constNumber = 30;
        
        public static List<string> logs = new List<string>();
        public static void LogAction(string action)
        {
            logs.Add(action);
        }
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
                clients[i].FillDepositAcc();
                clients[i].Open<AnotherAccount>();
                clients[i].FillAnotherAcc();
            }

            return clients;
        }
        public static List<Client> clientsList = GenerateClients();
    }
}
