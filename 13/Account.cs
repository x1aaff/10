namespace _13
{
    class Account : Client
    {
        public static event NotifyDelegate AccountNotify;
        private static int count = 0;
        public int Id { get; set; }
        public double Worth { get; set; }

        public Account(double worth = 0)
        {
            Id = ++count;
            Worth = worth;
        }
        public bool SendTransfer(Client toClient, Account toAccount, double sum)
        {
            if (sum > Worth)
            {
                return false;
            }
            IPushable<Account> pushableAccounts = new Stack<Client>(toClient, toAccount);
            pushableAccounts.PushWithSum(sum);
            Worth -= sum;
            string notificationMsg = $"Со счёта id{this.Id} {sum} а.д. направлено " +
                $"на счёт id{toAccount.Id} клиента {toClient.PhoneNumber}.";
            AccountNotify?.Invoke(notificationMsg);
            return true;
        }

        static public void Deposit<T>(double putValue, T targetAccount)
            where T : Account
        {
            IPut<Account> put = new Put<T>(putValue, targetAccount);
            put.TargetAccount.Worth += put.PutWorth;
            string notificationMsg = $"Счёт id{targetAccount.Id} пополнен на {put.PutWorth} а.д.";
            AccountNotify?.Invoke(notificationMsg);
        }
    }
    interface IPushable<in V>
    {
        void PushWithSum(double sum);
    }
    class Stack<V> : IPushable<V>
        where V : Client
    {
        private V toClient;
        private V toAccount;
        public V ToClient { set { toClient = value; } }
        public V ToAccount { set { toAccount = value; } }
        public Stack(V toClient, V toAccount)
        {
            this.toClient = toClient;
            this.toAccount = toAccount;
        }
        public void PushWithSum(double sum)
        {
            toClient.ClientAccounts.Find(x => x == toAccount).Worth += sum;
        }
    }

    interface IPut<out T>
    {
        T TargetAccount { get; }
        double PutWorth { get; }
    }
    
    class Put<T> : IPut<T>
    {
        private double putWorth;
        private T targetAccount;
        public double PutWorth
        {
            get { return putWorth; }
        }
        public T TargetAccount
        {
            get { return targetAccount; }
        }

        public Put(double putWorth, T targetAccount)
        {
            this.putWorth = putWorth;
            this.targetAccount = targetAccount;
        }
    }

    class DepositAccount : Account
    {
        //логика депозитного
        public DepositAccount() : base() { }
    }

    class AnotherAccount : Account
    {
        //логика недепозитного
        public AnotherAccount() : base() { }
    }
}
