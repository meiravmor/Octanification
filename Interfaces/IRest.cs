using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octanification.Interfaces
{
    interface IRest
    {
        void authenticate();

        Object fetchOctanEntities();

        Object fetchOctanUsers();

        void logoof();
    }
}
