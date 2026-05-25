using System;
using Domain.Entities;

namespace Application.Interfaces;


public interface IUserAccessor
{
    string? GetCurrentUserId();
    bool IsAuthenticated();
}
