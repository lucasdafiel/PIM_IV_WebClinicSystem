using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
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
            // RN-09: O sistema não deve permitir o agendamento de duas consultas no mesmo dia e horário para o mesmo profissional.
            // Verifica se já existe uma consulta para o profissional na mesma data/hora.
            var inicioSolicitado = request.Consulta.DataHoraInicio;
            var fimSolicitado = request.Consulta.DataHoraInicio.AddHours(1); // Considera duração padrão de 1 hora

            var consultasExistentes = await _unitOfWork.Consultas.GetConsultasPorPeriodoEProfissionalAsync(
                inicioSolicitado,
                fimSolicitado,
                request.Consulta.IdProfissional
            );

            // Se existir alguma consulta com DataHoraInicio exatamente igual, lança exceção de negócio
            if (consultasExistentes != null && consultasExistentes.Any(c => c.DataHoraInicio == inicioSolicitado))
            {
                throw new InvalidOperationException("Já existe uma consulta agendada para este profissional neste horário.");
            }

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