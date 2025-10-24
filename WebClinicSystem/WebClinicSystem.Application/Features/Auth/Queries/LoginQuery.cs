using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Application.Features.Auth.Queries
{
    public record LoginQuery(string Email, string Password) : IRequest<string>;
}
