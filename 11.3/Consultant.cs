using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._3
{
    public interface IAccess
    {
        //для генерации все права
        static byte nameRights = 2;
        static byte phoneNumberRights = 2;
        static byte idRights = 2;
        //инициализация пользователя
        void Login();
        //для записи на диск при выходе необходимы права
        void Logout()
        {
            nameRights = 2;
            phoneNumberRights = 2;
            idRights = 2;
        }
    }
    public class Consultant : IAccess
    {
        //0: no rights
        //1: r only
        //2: rw
        void IAccess.Login()
        {
            IAccess.nameRights = 1;
            IAccess.phoneNumberRights = 2;
            IAccess.idRights = 0;
        }
    }
}
