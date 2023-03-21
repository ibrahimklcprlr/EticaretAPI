using EticaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Aplication.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandle : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
         readonly  UserManager<AppUser> _userManager;

        public CreateUserCommandHandle(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
          IdentityResult result= await  _userManager.CreateAsync(new()
            {
                Id=Guid.NewGuid().ToString(),
                UserName=request.Username,
                NameSurName=request.NameSurname,
                Email=request.Email,
               
            },request.Password);
            CreateUserCommandResponse response= new CreateUserCommandResponse(){ success= result.Succeeded};
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
    }
}
