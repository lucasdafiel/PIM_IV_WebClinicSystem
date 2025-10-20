using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClinicSystem.Domain.Interfaces
{
    public interface IAuthService
    {
        //Método para computar o hash SHA-256 de uma senha.
        string ComputeSha256Hash(string password);
    }
}
