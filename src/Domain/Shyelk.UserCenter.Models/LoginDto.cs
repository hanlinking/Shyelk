using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shyelk.UserCenter.Models
{
    public class LoginDto
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string IsRemeber { get; set; }
        public string VerifyCode { get; set; }
    }
}
