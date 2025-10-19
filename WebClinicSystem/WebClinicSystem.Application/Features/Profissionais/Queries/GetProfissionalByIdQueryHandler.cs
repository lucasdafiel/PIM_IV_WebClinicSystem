using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Application.Features.Profissionais.DTOs;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Profissionais.Queries
{
    public class GetProfissionalByIdQueryHandler : IRequestHandler<GetProfissionalByIdQuery, ProfissionalDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfissionalByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProfissionalDto?> Handle(GetProfissionalByIdQuery request, CancellationToken cancellationToken)
        {
            // Usa o repositório para buscar o profissional no banco.
            var profissional = await _unitOfWork.Profissionais.GetByIdAsync(request.Id);

            // Se não encontrar, retorna nulo.
            if (profissional is null)
            {
                return null;
            }

            // Mapeia a entidade para o DTO de resposta.
            return new ProfissionalDto(
                profissional.ProfissionalId,
                profissional.NomeCompleto,
                profissional.Especialidade,
                profissional.RegistroConselho,
                profissional.Telefone);
        }
    }
}
