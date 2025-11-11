using System.ComponentModel.DataAnnotations;

namespace WebClinicSystem.Application.Features.Pacientes.DTOs
{
    // (Este arquivo foi substituído)
    // DTO para a atualização de um paciente.
    // Contém apenas os campos que podem ser alterados,
    // e possui um construtor vazio para o Model Binding do Razor.
    public class UpdatePacienteDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone deve ter entre 10 e 15 caracteres")]
        public string TelefoneContato { get; set; }
    }
}