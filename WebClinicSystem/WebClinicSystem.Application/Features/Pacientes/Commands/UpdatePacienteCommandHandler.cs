using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Features.Pacientes.Commands
{
    public class UpdatePacienteCommandHandler : IRequestHandler<UpdatePacienteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePacienteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdatePacienteCommand request, CancellationToken cancellationToken)
        {
            // 1. Busca o paciente existente no banco de dados.
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(request.PacienteId);

            // 2. Se não encontrar, pode lançar uma exceção (a ser tratada pelo controller).
            if (paciente is null)
            {
                // No futuro, podemos criar exceções personalizadas.
                throw new KeyNotFoundException("Paciente não encontrado.");
            }

            // Verifica se o CPF está sendo alterado para um que já existe.
            if (paciente.Cpf != request.Cpf)
            {
                var outroPacienteComCpf = await _unitOfWork.Pacientes.GetByCpfAsync(request.Cpf);
                if (outroPacienteComCpf is not null)
                {
                    throw new InvalidOperationException("O CPF informado já está em uso por outro paciente.");
                }
            }

            // 3. Atualiza as propriedades do objeto que veio do banco.
            paciente.NomeCompleto = request.NomeCompleto;
            paciente.Cpf = request.Cpf;
            paciente.DataNascimento = request.DataNascimento;
            paciente.TelefoneContato = request.TelefoneContato;
            paciente.Email = request.Email;

            // 4. Salva as alterações. O EF Core detecta as mudanças e as envia para o banco.
            await _unitOfWork.CompleteAsync();
        }
    }
}
