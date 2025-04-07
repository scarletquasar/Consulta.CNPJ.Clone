using Consulta.CNPJ.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consulta.CNPJ.Services
{
    public interface ICNPJService
    {
        CNPJResult ConsultarCPNJAlfanumerico(string cnpj);
        Task<CNPJResult> ConsultarCPNJAlfanumericoAsync(string cnpj);
    }
}