using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pharmacymansys
{
    class LoginName
    {
        static string nm;
        public static string name
        {
            get
            {
                return nm;
            }
            set
            {
                nm = value;
            }
        }
    }
}
