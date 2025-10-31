using MediatR;
using System;

namespace WebClinicSystem.Application.Features.Consultas.Commands
{
    // Command para cancelar uma consulta
    public class CancelarConsultaCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public CancelarConsultaCommand(int id)
        {
            Id = id;
        }
    }
}