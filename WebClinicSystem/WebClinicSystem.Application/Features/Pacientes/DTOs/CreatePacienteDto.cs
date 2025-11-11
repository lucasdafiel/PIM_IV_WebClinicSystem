using System;
using System.ComponentModel.DataAnnotations;

namespace WebClinicSystem.Application.Features.Pacientes.DTOs
{
    // DTO para a criação de um novo paciente.
    // (Este arquivo estava faltando)
    public class CreatePacienteDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 caracteres")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone deve ter entre 10 e 15 caracteres")]
        public string TelefoneContato { get; set; }
    }
}