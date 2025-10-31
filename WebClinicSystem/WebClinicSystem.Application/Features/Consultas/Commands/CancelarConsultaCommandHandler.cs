using MediatR;
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
            // Busca a consulta pelo Id
            var consulta = await _unitOfWork.Consultas.GetByIdAsync(request.Id);

            if (consulta == null)
                return false; // Consulta não encontrada

            // Altera o status para "Cancelada"
            consulta.Status = "Cancelada";

            // Salva as alterações
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}