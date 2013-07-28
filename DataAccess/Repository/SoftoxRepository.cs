using Common.BaseClass;
using Common.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    abstract public class SoftoxRepository<T> : BaseRepository<T>
        where T : class
    {
        public SoftoxRepository()
            : base(SettingsHelper.ConnectionString) { }
    }
}