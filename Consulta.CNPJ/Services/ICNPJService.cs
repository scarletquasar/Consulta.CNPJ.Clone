using Consulta.CNPJ.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consulta.CNPJ.Services
{
    public interface ICNPJService
    {
        CNPJResult ConsultarCNPJ(string cnpj);
        Task<CNPJResult> ConsultarCNPJAsync(string cnpj);
        bool ValidarCNPJAlfanumerico(string cnpj);
        string ConverterParaNumerico(string cnpj);
        int CalcularDV(string cnpj, int[] pesos);
    }
}