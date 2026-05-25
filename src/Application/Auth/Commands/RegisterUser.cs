using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands;

public class RegisterUser
{
    public class Command : IRequest<string>
    {
        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string DisplayName { get; set; }
    }

    public class Handler(UserManager<AppUser> userManager)
        : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var existingUser =
                await userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.Email,
                DisplayName = request.DisplayName
            };

            var result = await userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user");
            }

            return "User registered successfully";
        }
    }
}