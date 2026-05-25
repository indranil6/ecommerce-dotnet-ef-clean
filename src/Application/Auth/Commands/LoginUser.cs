using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands;

public class LoginUser
{
    public class Command : IRequest<string>
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }

    public class Handler(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService)
        : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var user =
                await userManager.FindByEmailAsync(request.Email) ?? throw new Exception("Invalid credentials");

            var result =
                await signInManager.CheckPasswordSignInAsync(
                    user,
                    request.Password,
                    false);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid credentials");
            }

            var token = tokenService.CreateToken(user);

            return token;
        }
    }
}