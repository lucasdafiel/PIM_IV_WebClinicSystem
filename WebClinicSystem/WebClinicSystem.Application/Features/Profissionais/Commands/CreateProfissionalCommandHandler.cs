using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Profissionais.Commands
{
    public class CreateProfissionalCommandHandler : IRequestHandler<CreateProfissionalCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProfissionalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateProfissionalCommand request, CancellationToken cancellationToken)
        {
            var profissional = new Profissional
            {
                NomeCompleto = request.NomeCompleto,
                Especialidade = request.Especialidade,
                RegistroConselho = request.RegistroConselho,
                Telefone = request.Telefone
            };

            await _unitOfWork.Profissionais.AddAsync(profissional);
            await _unitOfWork.CompleteAsync();

            return profissional.ProfissionalId;
        }
    }
}
