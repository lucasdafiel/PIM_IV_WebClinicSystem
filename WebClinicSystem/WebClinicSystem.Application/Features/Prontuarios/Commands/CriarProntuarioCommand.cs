using MediatR;
using WebClinicSystem.Application.Features.Prontuarios.DTOs;

namespace WebClinicSystem.Application.Features.Prontuarios.Commands
{
    // Command para criar um prontuário
    public class CriarProntuarioCommand : IRequest<int>
    {
        public CreateProntuarioDTO Prontuario { get; set; }

        public CriarProntuarioCommand(CreateProntuarioDTO prontuario)
        {
            Prontuario = prontuario;
        }
    }
}
