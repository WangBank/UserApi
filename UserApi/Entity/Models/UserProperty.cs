using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Api.Models
{
    public class UserProperty
    {
        public int UserId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
