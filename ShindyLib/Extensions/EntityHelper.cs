using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventLibrary.Entities;

namespace EventLibrary.Extensions
{
    public static class EntityHelper
    {
        /// <summary>
        /// Returns true if the criteria provided in the predicate is true
        /// </summary>
        /// <param name="p"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool IsPersonInRole(this Person p, Func<Role, bool> predicate)
        {
            return p.Roles.HasContent() && p.Roles.Where(predicate).Any();
        }
    }
}
