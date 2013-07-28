using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Status
{
    public enum TopStatus : byte
    {
        InUse = 1,
        ToValidate = 2,
        Deleted = 3
    }
}