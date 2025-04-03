```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace Consulta.CNPJ.Helpers
{
    internal class Validacao
    {
        internal static bool ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 12)
                return false;
            tempCnpj = ConverterParaNumerico(cnpj.Substring(0, 12));
            if (tempCnpj.Length != 12)
                return false;
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        private static string ConverterParaNumerico(string cnpj)
        {
            return string.Concat(cnpj.Select(c =>
            {
                if (char.IsDigit(c)) return c - '0';
                if (char.IsLetter(c)) return (c - 'A') + 17;
                return -1; // Caracter inválido
            }).Where(v => v >= 0));
        }
    }
}
```