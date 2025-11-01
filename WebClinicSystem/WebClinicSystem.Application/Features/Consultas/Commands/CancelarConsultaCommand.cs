using MediatR;

namespace WebClinicSystem.Application.Features.Consultas.Commands
{
    // Command para cancelar uma consulta
    public class CancelarConsultaCommand : IRequest<bool>
    {
        // Id da consulta a ser cancelada
        public int IdConsulta { get; set; }

        public CancelarConsultaCommand(int idConsulta)
        {
            IdConsulta = idConsulta;
        }
    }
}