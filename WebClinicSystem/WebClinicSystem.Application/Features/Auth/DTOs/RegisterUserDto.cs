using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Auth.DTOs
{
    public record RegisterUserDto(
        string Email,
        string Password,
        int PerfilId);
}
