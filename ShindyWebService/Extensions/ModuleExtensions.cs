using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using SimpleSocialAuth.MVC3;

namespace EventWebService
{
    /// <summary>
    /// Used to implement extension/helper methods for module classes. Cleaner code organization
    /// </summary>
    public static class ModuleExtensions
    {
        public static AuthType DetermineAuthType(this PersonModule module, int authRequest)
        {
            if(authRequest == (int)AuthType.Google)
            {
                return AuthType.Google;
            }
            else if (authRequest == (int)AuthType.Facebook)
            {
                return AuthType.Facebook;
            }
            else if (authRequest == (int)AuthType.Twitter)
            {
                return AuthType.Twitter;
            }
            else
            {
                return AuthType.Unknown;
            }
        }

        /// <summary>
        /// Standard encryption
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SecureString(this string value)
        {  
            return BCrypt.Net.BCrypt.HashPassword(value);
        }

        /// <summary>
        /// Super strong encryption depending on workfactor size, performance hit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="workFactor"></param>
        /// <returns></returns>
        public static string SecureString(this string value, int workFactor)
        {
            return BCrypt.Net.BCrypt.HashPassword(value, workFactor);
        }
    }
}