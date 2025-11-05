using System.Threading.Tasks;
// Importa os DTOs presumidos da camada Application
using WebClinicSystem.Application.Features.Prontuarios.DTOs;

namespace WebClinicSystem.Web.Services
{
    public interface IProntuarioApiService
    {
        /// <summary>
        /// Cria um novo prontuário na API.
        /// </summary>
        /// <param name="dto">Os dados do prontuário a ser criado.</param>
        Task CreateAsync(CreateProntuarioDTO dto);

        /// <summary>
        /// Busca um prontuário existente com base no ID da consulta associada.
        /// </summary>
        /// <param name="idConsulta">O ID da consulta.</param>
        /// <returns>O ProntuarioDTO ou null se não for encontrado.</returns>
        Task<ProntuarioDTO> GetByConsultaIdAsync(int idConsulta);
    }
}