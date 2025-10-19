using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

namespace WebClinicSystem.Application.Feature.Pacientes.Commands
{
    public class CadastrarPacienteCommandHandler : IRequestHandler<CadastrarPacienteCommand, Guid>
    {
        // O handler depende da INTERFACE do repositório, não da implementação!
        // Isso é a Inversão de Dependência (o 'D' do SOLID) em ação.
        private readonly IPacienteRepository _pacienteRepository;

        public CadastrarPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Guid> Handle(CadastrarPacienteCommand request, CancellationToken cancellationToken)
        {
            // 1. Cria a entidade de domínio com os dados do comando.
            var paciente = new Paciente(
                request.NomeCompleto,
                request.Cpf,
                request.DataNascimento,
                request.TelefoneContato);

            // 2. Usa a abstração do repositório para adicionar o paciente.
            await _pacienteRepository.AddAsync(paciente);

            // 3. (Futuramente, aqui chamaremos o Unit of Work para salvar as alterações)

            // 4. Retorna o ID da entidade recém-criada.
            return paciente.Id;
        }
    }
}
