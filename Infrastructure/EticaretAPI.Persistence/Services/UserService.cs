using EticaretAPI.Aplication.Abstraction.Services;
using EticaretAPI.Aplication.DTOs.User;
using EticaretAPI.Aplication.Exceptions;
using EticaretAPI.Aplication.Features.Commands.User.CreateUser;
using EticaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _usermanager;

        public UserService(UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser user)
        {
            IdentityResult result = await _usermanager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.Username,
                NameSurName = user.NameSurname,
                Email = user.Email,

            }, user.Password);
            CreateUserResponse response = new() { success = result.Succeeded };
            if (result.Succeeded)
            {
                response.message = "Kullanıcı Başarılı Bir Şekilde Eklendi";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.message += $"{error.Code} - {error.Description}";
                }

            }
            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate,int refreshTokenLifeTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenLifeTime);
               await _usermanager.UpdateAsync(user);
            }
            else
            {
                throw new NotFoundUserException();
            }
            
        }
    }
}
