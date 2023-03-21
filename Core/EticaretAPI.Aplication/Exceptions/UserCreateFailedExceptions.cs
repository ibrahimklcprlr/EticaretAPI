using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Exceptions
{
    public class UserCreateFailedExceptions:Exception
    {

        public UserCreateFailedExceptions()
        {
            
        }
        public UserCreateFailedExceptions(string? message) : base(message)
        {
            
        }
        public UserCreateFailedExceptions(string?message,Exception? exception):base(message,exception) 
        {
            
        }
    }
}
