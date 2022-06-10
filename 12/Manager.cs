namespace _12
{
    public class Manager : IAccess
    {
        void IAccess.Login()
        {
            IAccess.nameRights = 2;
            IAccess.phoneNumberRights = 2;
            IAccess.idRights = 2;
        }
        public static void AddClient(string lastName, string firstName, string patronymicName, string phoneNumber, string idNumber,
            out bool error)
        {
            bool empty = string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(patronymicName) ||
                string.IsNullOrEmpty(phoneNumber) ||
                string.IsNullOrEmpty(idNumber);
            if (!empty)
            {
                Client.clients.Add(new Client(lastName, firstName, patronymicName, phoneNumber, idNumber));
                error = false;
            }
            else
            {
                error = true;
            }
        }
    }
}
