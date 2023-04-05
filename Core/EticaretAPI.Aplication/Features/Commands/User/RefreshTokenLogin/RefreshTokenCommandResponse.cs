using EticaretAPI.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.User.RefreshTokenLogin
{
    public class RefreshTokenCommandResponse
    {
        public Token token { get; set; }
    }
}
