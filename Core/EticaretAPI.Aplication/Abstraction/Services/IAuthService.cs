using EticaretAPI.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Abstraction.Services
{
    public interface IAuthService
    {
        Task<DTOs.Token> LoginAsync(string UsernameOrEmail,string Password,int AccesTokenLifeTime);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);

    }
}
