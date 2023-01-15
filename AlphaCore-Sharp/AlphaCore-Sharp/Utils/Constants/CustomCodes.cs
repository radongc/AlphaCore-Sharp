using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Utils.Constants
{
    internal class CustomCodes
    {
        public enum LoginStatus : int
        {
            INVALID_PASSWORD = -1,
            UNKNOWN_ACCOUNT = 0,
            SUCCESS = 1
        }
    }
}
