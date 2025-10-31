using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Consultas.Commands
{
    // Handler para agendar uma nova consulta
    public class AgendarConsultaCommandHandler : IRequestHandler<AgendarConsultaCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgendarConsultaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AgendarConsultaCommand request, CancellationToken cancellationToken)
        {
            // Cria a entidade Consulta
            var consulta = new Consulta
            {
                DataHoraInicio = request.Consulta.DataHoraInicio,
                DataHoraFim = request.Consulta.DataHoraInicio.AddHours(1),
                PacienteId = request.Consulta.IdPaciente,
                ProfissionalId = request.Consulta.IdProfissional,
                Status = "Agendada"
            };

            // Adiciona ao repositório
            await _unitOfWork.Consultas.AddAsync(consulta);

            // Salva as alterações
            await _unitOfWork.CompleteAsync();

            // Retorna o Id da consulta agendada
            return consulta.ConsultaId;
        }
    }
}