using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Exceptions
{
    public class NotFoundUserException:Exception
    {
        public NotFoundUserException():base("Kullanıcı Adı veya şifre Hatalı")
        {
            
        }
        public NotFoundUserException(string? message):base(message)
        {
            
        }
        public NotFoundUserException(string? message,Exception?ex):base(message,ex) { }
       
    }
}
