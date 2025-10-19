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
    public class GetAllProfissionaisQueryHandler : IRequestHandler<GetAllProfissionaisQuery, IEnumerable<ProfissionalDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProfissionaisQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProfissionalDto>> Handle(GetAllProfissionaisQuery request, CancellationToken cancellationToken)
        {
            var profissionais = await _unitOfWork.Profissionais.GetAllAsync();

            return profissionais.Select(p => new ProfissionalDto(
                p.ProfissionalId,
                p.NomeCompleto,
                p.Especialidade,
                p.RegistroConselho,
                p.Telefone));
        }
    }
}
