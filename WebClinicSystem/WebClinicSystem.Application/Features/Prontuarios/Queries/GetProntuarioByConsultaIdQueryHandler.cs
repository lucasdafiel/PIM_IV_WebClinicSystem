using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Prontuarios.DTOs;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Prontuarios.Queries
{
    // Handler para buscar um prontuário por Id da consulta
    public class GetProntuarioByConsultaIdQueryHandler : IRequestHandler<GetProntuarioByConsultaIdQuery, ProntuarioDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProntuarioByConsultaIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProntuarioDTO> Handle(GetProntuarioByConsultaIdQuery request, CancellationToken cancellationToken)
        {
            var prontuario = await _unitOfWork.Prontuarios.GetByConsultaIdAsync(request.IdConsulta);

            if (prontuario == null)
                return null;

            // Mapeia para DTO incluindo nome do profissional via consulta
            return new ProntuarioDTO
            {
                ProntuarioId = prontuario.ProntuarioId,
                Descricao = prontuario.Descricao,
                DataCriacao = prontuario.DataRegistro,
                NomeProfissional = prontuario.Consulta?.Profissional?.NomeCompleto ?? string.Empty
            };
        }
    }
}
