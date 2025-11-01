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
            // RN-12: Um prontuário só pode ser criado para uma consulta com status 'Concluída'.
            // Busca a consulta relacionada usando o repositório
            var consulta = await _unitOfWork.Consultas.GetByIdAsync(request.Prontuario.IdConsulta);

            // Se a consulta não for encontrada, lança exceção de chave não encontrada
            if (consulta == null)
                throw new KeyNotFoundException("Consulta não encontrada.");

            // Verifica o status da consulta
            if (!string.Equals(consulta.Status, "Concluída", StringComparison.OrdinalIgnoreCase))
            {
                // Se não estiver concluída, bloqueia a operação conforme regra de negócio
                throw new InvalidOperationException("Prontuários só podem ser criados para consultas já concluídas.");
            }

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
