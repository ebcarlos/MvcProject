using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    /// <summary>
    /// Class to manage status contained in enumerations
    /// </summary>
    public static class StatusHelper
    {
        /// <summary>
        /// Get members from the enum T (name & values)
        /// </summary>
        public static Dictionary<byte, string> GetList<T>()
        {
            var list = new Dictionary<byte, string>();
            var names = Enum.GetNames(typeof(T));
            var values = Enum.GetValues(typeof(T));

            for (int i = 0; i < names.Length; i++)
                list.Add((byte)values.GetValue(i), names[i]);

            return list;
        }

        /// <summary>
        /// Get the name of a member from the enum T
        /// Return null if the member doesn't exist
        /// </summary>
        public static string GetName<T>(byte value)
        {
            return Enum.GetName(typeof(T), value);
        }

        /// <summary>
        /// Return true if value is a member of the enum T
        /// </summary>
        public static bool IsDefined<T>(byte value)
        {
            return Enum.IsDefined(typeof(T), value);
        }
    }
}