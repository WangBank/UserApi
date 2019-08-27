using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Dtos
{
    public class UserIndentity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
