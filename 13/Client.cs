using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _13
{
    class Client
    {
        public static event NotifyDelegate Notify;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<Account> ClientAccounts { get; set; }

        public Client(string firstName, string lastName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            ClientAccounts = new List<Account>();
        }
        public Client()
        {

        }
        public void Open<T>()
            where T : Account, new()
        {
            var a = new T();
            ClientAccounts.Add(a);
            string notificationMsg = $"{DateTime.Now.ToShortTimeString()}: " +
                $"Открыт {typeof(T)} с id{a.Id} для клиента {this.PhoneNumber}.";
            Notify?.Invoke(notificationMsg);
        }
        public void Close<T>(T account)
            where T : Account
        {
            Debug.WriteLine(account.Id);
            string notificationMsg = $"{DateTime.Now.ToShortTimeString()}: " +
                $"Закрыт {typeof(T)} с id{account.Id} для клиента {this.PhoneNumber}." +
                $"\n На счёте было {account.Worth} а.д.";
            Notify?.Invoke(notificationMsg);
            this.ClientAccounts.Remove(account);
        }
        public List<Account> GetPossibleTransfers(Account fromAccount)
        {
            List<Account> accounts = new List<Account>();
            accounts.AddRange(ClientAccounts);
            accounts.Remove(fromAccount);
            return accounts;
        }
    }
}
