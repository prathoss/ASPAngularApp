using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingAppAPI.StaticClasses
{
    public static class Keys
    {
        public static byte[] JwtSecurityKey
        {
            get
            {
                return Encoding.ASCII.GetBytes("SecriteKeyForVerification");
            }
        }
        
    }
}
