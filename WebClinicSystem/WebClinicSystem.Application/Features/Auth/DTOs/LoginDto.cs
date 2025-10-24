using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Auth.DTOs
{
    // DTO para receber as credenciais de login.
    public record LoginDto(string Email, string Password);
}
