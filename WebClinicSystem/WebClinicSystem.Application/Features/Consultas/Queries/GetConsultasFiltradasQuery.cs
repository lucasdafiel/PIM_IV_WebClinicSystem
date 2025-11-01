using MediatR;
using System;
using System.Collections.Generic;
using WebClinicSystem.Application.Features.Consultas.DTOs;

namespace WebClinicSystem.Application.Features.Consultas.Queries
{
    // Query para buscar consultas filtradas por profissional e período (RF-05)
    public class GetConsultasFiltradasQuery : IRequest<IEnumerable<ConsultaDTO>>
    {
        // Id do profissional (opcional)
        public int? IdProfissional { get; set; }

        // Data inicial do filtro (opcional)
        public DateTime? DataInicio { get; set; }

        // Data final do filtro (opcional)
        public DateTime? DataFim { get; set; }
    }
}
