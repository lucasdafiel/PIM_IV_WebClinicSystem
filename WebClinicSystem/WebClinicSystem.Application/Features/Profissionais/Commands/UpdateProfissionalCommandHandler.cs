using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Profissionais.Commands
{
    public class UpdateProfissionalCommandHandler : IRequestHandler<UpdateProfissionalCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfissionalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateProfissionalCommand request, CancellationToken cancellationToken)
        {
            var profissional = await _unitOfWork.Profissionais.GetByIdAsync(request.ProfissionalId);

            if (profissional is null) throw new KeyNotFoundException("Profissional não encontrado.");

            profissional.NomeCompleto = request.NomeCompleto;
            profissional.Especialidade = request.Especialidade;
            profissional.Telefone = request.Telefone;

            _unitOfWork.Profissionais.Update(profissional);
            await _unitOfWork.CompleteAsync();
        }
    }
}
