using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi
{
    public class UserOperatorExpection:Exception
    {
        public UserOperatorExpection() { }
        public UserOperatorExpection(string message):base(message)
        {

        }
        public UserOperatorExpection(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
