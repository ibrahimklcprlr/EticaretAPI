using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.User.CreateUser
{
    public class CreateUserCommandResponse
    {
        public bool success { get; set; }
        public string? message { get; set; }
    }
}
