using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossyStore.Core.Interfaces.User_Interface
{
    public interface IUser
    {
        bool CheckUser(string username, string password);
    }
}
