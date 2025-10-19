using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClinicSystem.Domain.Entities;

namespace WebClinicSystem.Domain.Interfaces
{
    public interface IProfissionalRepository
    {
        Task<Profissional> GetByIdAsync(int id);
        Task<IEnumerable<Profissional>> GetAllAsync();
        Task AddAsync(Profissional profissional);
        void Update(Profissional profissional); // Adicionado para o PUT
        void Delete(Profissional profissional); // Adicionado para o DELETE
    }
}
