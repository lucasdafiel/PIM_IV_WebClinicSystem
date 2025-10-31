using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Prontuarios.Commands
{
    // Handler responsável por criar o prontuário
    public class CriarProntuarioCommandHandler : IRequestHandler<CriarProntuarioCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CriarProntuarioCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CriarProntuarioCommand request, CancellationToken cancellationToken)
        {
            // Cria a entidade Prontuario a partir do DTO
            var prontuario = new Prontuario
            {
                Descricao = request.Prontuario.Descricao,
                DataRegistro = DateTime.Now,
                ConsultaId = request.Prontuario.IdConsulta
            };

            // Adiciona ao repositório
            await _unitOfWork.Prontuarios.AddAsync(prontuario);

            // Persiste as alterações
            await _unitOfWork.CompleteAsync();

            return prontuario.ProntuarioId;
        }
    }
}
