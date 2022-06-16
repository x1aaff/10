using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _13
{
    class Client
    {
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

        /*public void Open()  //old
        {
            ClientAccounts.Add(new Account());
        }*/
        public void Open<T>()
            where T : Account, new()
        {
            ClientAccounts.Add(new T());
        }
        /*public void Close(object account) //old
        {
            ClientAccounts.Remove((Account)account);
        }*/
        public void Close<T>(T account)
            where T : Account
        {
            Debug.WriteLine(account.Id);
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
