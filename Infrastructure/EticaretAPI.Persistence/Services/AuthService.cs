using EticaretAPI.Aplication.Abstraction.Services;
using EticaretAPI.Aplication.Abstraction.Token;
using EticaretAPI.Aplication.DTOs;
using EticaretAPI.Aplication.Exceptions;
using EticaretAPI.Aplication.Features.Commands.User.LoginUser;
using EticaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
        }

        public async Task<Token> LoginAsync(string UsernameOrEmail, string Password, int AccesTokenLifeTime)
        {
            AppUser? user = await _userManager.FindByNameAsync(UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(UsernameOrEmail);
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,Password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(AccesTokenLifeTime,user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5);
                return token;
                
            }
            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
           AppUser? user=await _userManager.Users.FirstOrDefaultAsync(c => c.RefreshToken == refreshToken);
            if (user!=null &&user.RefreshTokenEndDate>DateTime.UtcNow)
            {
               Token token= _tokenHandler.CreateAccessToken(15,user);
                _userService?.UpdateRefreshToken(token.RefreshToken,user,token.Expiration, 300);
                return token;
            }
            else
            {
                throw new NotFoundUserException();
            }
        }
    }
}
