using MediatR;
using WebClinicSystem.Application.Features.Pacientes.Commands;
using WebClinicSystem.Domain.Entities;
using WebClinicSystem.Domain.Interfaces;

public class CadastrarPacienteCommandHandler : IRequestHandler<CadastrarPacienteCommand, int>
{
    // Agora dependemos do IUnitOfWork, que é mais completo.
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarPacienteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CadastrarPacienteCommand request, CancellationToken cancellationToken)
    {
        var pacienteExistente = await _unitOfWork.Pacientes.GetByCpfAsync(request.Cpf);

        if (pacienteExistente is not null)
        {
            // Lança uma exceção se o CPF já estiver em uso.
            // O controller irá capturar isso e retornar um erro 400 (Bad Request).
            throw new InvalidOperationException("O CPF informado já está cadastrado no sistema.");
        }

        var paciente = new Paciente
        {
            NomeCompleto = request.NomeCompleto,
            Cpf = request.Cpf,
            DataNascimento = request.DataNascimento,
            TelefoneContato = request.TelefoneContato,
            DataCadastro = DateTime.UtcNow
        };

        // Adicionamos o paciente usando o repositório DENTRO do Unit of Work.
        await _unitOfWork.Pacientes.AddAsync(paciente);

        // Agora, chamamos o "caixa" para finalizar a compra e salvar TUDO no banco.
        await _unitOfWork.CompleteAsync();

        return paciente.PacienteId;
    }
}