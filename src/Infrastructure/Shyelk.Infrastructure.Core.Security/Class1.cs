using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Shyelk.Infrastructure.Core.Security
{
    public class Class1
    {
        public Class1()
        {
            RSA rsa=RSA.Create();
            rsa.ExportParameters(false);
        }
    }
}
