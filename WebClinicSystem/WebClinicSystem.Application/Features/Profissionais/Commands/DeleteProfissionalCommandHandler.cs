using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Profissionais.Commands
{
    public class DeleteProfissionalCommandHandler : IRequestHandler<DeleteProfissionalCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProfissionalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProfissionalCommand request, CancellationToken cancellationToken)
        {
            var profissional = await _unitOfWork.Profissionais.GetByIdAsync(request.Id);

            if (profissional is not null)
            {
                _unitOfWork.Profissionais.Delete(profissional);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
