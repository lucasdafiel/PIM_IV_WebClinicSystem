using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Pacientes.Commands
{
    public class DeletePacienteCommandHandler : IRequestHandler<DeletePacienteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePacienteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeletePacienteCommand request, CancellationToken cancellationToken)
        {
            // 1. Busca o paciente que será excluído.
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(request.PacienteId);

            // 2. Se encontrar, remove.
            if (paciente is not null)
            {
                _unitOfWork.Pacientes.Delete(paciente);
                await _unitOfWork.CompleteAsync(); // Salva a remoção no banco.
            }
            // Se não encontrar, a operação simplesmente termina sem erro,
            // pois o objetivo (não ter o registro no banco) foi alcançado.
        }
    }
}
