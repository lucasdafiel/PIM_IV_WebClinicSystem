using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Consultas.Commands
{
    // Handler para cancelar uma consulta existente
    public class CancelarConsultaCommandHandler : IRequestHandler<CancelarConsultaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelarConsultaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CancelarConsultaCommand request, CancellationToken cancellationToken)
        {
            // Carrega a consulta usando o repositório (por IdConsulta)
            var consulta = await _unitOfWork.Consultas.GetByIdAsync(request.IdConsulta);

            // Se não encontrada, retorna false conforme contrato
            if (consulta == null)
                return false;

            // RN-11: Apenas consultas com status 'Agendada' podem ser canceladas
            if (!string.Equals(consulta.Status, "Agendada", StringComparison.OrdinalIgnoreCase))
            {
                // Lança exceção de negócio informando que não é possível cancelar
                throw new InvalidOperationException("A consulta não pode ser cancelada.");
            }

            // Atualiza o status para 'Cancelada'
            consulta.Status = "Cancelada";

            // Salva as alterações no banco via UnitOfWork
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}