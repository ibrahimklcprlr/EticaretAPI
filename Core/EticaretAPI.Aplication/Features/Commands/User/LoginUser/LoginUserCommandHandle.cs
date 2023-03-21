using EticaretAPI.Aplication.Abstraction.Token;
using EticaretAPI.Aplication.DTOs;
using EticaretAPI.Aplication.Exceptions;
using EticaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.User.LoginUser
{
    public class LoginUserCommandHandle : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandle(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
           AppUser? user=await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            if (user == null)
            {
                throw new NotFoundUserException();
            }
            SignInResult result=await _signInManager.CheckPasswordSignInAsync(user,request.Password,false);
            if (result.Succeeded)
            {
               Token token= _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse()
                {
                     Token= token
                };
            }
             throw new AuthenticationErrorException();
        }
    }
}
