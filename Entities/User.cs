using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octanification.Entities
{
    class User
    {
        public string email { get; set; }
        public string uuid { get; set; }
        public string fullName { get; set; }

        public User (string i_email, string i_uuid, string i_fullNAme)
        {
            email = i_email;
            uuid = i_uuid;
            fullName = i_fullNAme;
        }
    }
}
