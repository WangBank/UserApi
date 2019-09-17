using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Users.Identity.Services
{
    public interface IAuthCodeService
    {
        Task<bool> Validate(string phone, string authCode);
    }
}
