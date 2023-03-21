using EticaretAPI.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.User.LoginUser
{
    public class LoginUserCommandResponse
    {
        
    }
    public class LoginUserSuccessCommandResponse: LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUserErrorCommandResponse: LoginUserCommandResponse
    {
        public string message { get; set; }
    }
}
