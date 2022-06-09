using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._2
{
    public class Consultant
    {
        //0: no rights
        //1: r only
        //2: rw
        private static byte nameRights = 1;
        private static byte phoneNumberRights = 2;
        private static byte idRights = 0;

        public static byte NameRights { get { return nameRights; } }
        public static byte PhoneNumberRights { get { return phoneNumberRights; } }
        public static byte IdRights { get { return idRights; } }
    }
}
