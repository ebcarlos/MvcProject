using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Status
{
    public enum UserStatus : byte
    {
        SuperAdmin = 1,
        Admin = 2,
        User = 3,
        Deleted = 4
    }
}