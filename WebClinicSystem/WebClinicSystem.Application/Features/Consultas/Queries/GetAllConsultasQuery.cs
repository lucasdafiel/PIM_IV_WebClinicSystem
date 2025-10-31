using MediatR;
using System.Collections.Generic;
using WebClinicSystem.Application.Features.Consultas.DTOs;

namespace WebClinicSystem.Application.Features.Consultas.Queries
{
    // Query para buscar todas as consultas
    public class GetAllConsultasQuery : IRequest<IEnumerable<ConsultaDTO>>
    {
    }
}