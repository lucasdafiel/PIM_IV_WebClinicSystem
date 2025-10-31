using MediatR;
using WebClinicSystem.Application.Features.Prontuarios.DTOs;
using System;

namespace WebClinicSystem.Application.Features.Prontuarios.Queries
{
    // Query para obter um prontuário pelo Id da consulta
    public class GetProntuarioByConsultaIdQuery : IRequest<ProntuarioDTO>
    {
        public int IdConsulta { get; set; }

        public GetProntuarioByConsultaIdQuery(int idConsulta)
        {
            IdConsulta = idConsulta;
        }
    }
}
