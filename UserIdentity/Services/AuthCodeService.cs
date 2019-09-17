using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Identity.Services
{
    public class AuthCodeService : IAuthCodeService
    {
        public async Task<bool> Validate(string phone, string authCode)
        {
            Func<bool> func = () => { return true; };
            return await Task.Run(func);
        }
    }
}
