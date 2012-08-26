using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EventTest
{
    /// <summary>
    /// Derived Assert class created to allow more helper methods
    /// </summary>
    public  class XUnitExtensions : Assert
    {  
        public static bool IsPositiveNumber(int num)
        {
            return num > 0;
        }
    }
}
