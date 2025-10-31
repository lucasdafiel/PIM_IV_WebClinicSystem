using MediatR;
using WebClinicSystem.Application.Features.Consultas.DTOs;

namespace WebClinicSystem.Application.Features.Consultas.Commands
{
    // Command para agendar uma consulta
    public class AgendarConsultaCommand : IRequest<int>
    {
        public CreateConsultaDTO Consulta { get; set; }

        public AgendarConsultaCommand(CreateConsultaDTO consulta)
        {
            Consulta = consulta;
        }
    }
}