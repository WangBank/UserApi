using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Api.Dtos
{
    public class UserIdentity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
