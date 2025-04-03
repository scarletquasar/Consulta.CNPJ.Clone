```csharp
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class CNPJService
{
    private readonly HttpClient _httpClient;

    public CNPJService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> ConsultarCNPJ(string cnpj)
    {
        if (!ValidarCNPJAlfanumerico(cnpj))
        {
            throw new ArgumentException("CNPJ alfanumérico inválido.");
        }

        string url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception("Erro ao consultar CNPJ.");
    }

    private bool ValidarCNPJAlfanumerico(string cnpj)
    {
        if (cnpj.Length != 12) return false;

        int[] weights1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] weights2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string numericCNPJ = ConvertToNumeric(cnpj);
        if (numericCNPJ.Length != 12) return false;

        int firstDV = CalculateDV(numericCNPJ, weights1);
        int secondDV = CalculateDV(numericCNPJ + firstDV, weights2);

        string completeCNPJ = numericCNPJ + firstDV + secondDV;

        return completeCNPJ.EndsWith(cnpj.Substring(cnpj.Length - 2));
    }

    private string ConvertToNumeric(string cnpj)
    {
        return string.Concat(cnpj.Select(c =>
        {
            if (char.IsDigit(c)) return c - '0';
            if (char.IsLetter(c)) return (c - 'A') + 17;
            return -1;
        }).Where(v => v >= 0));
    }

    private int CalculateDV(string cnpj, int[] weights)
    {
        int sum = cnpj.Select((c, index) => (c - '0') * weights[index]).Sum();
        int remainder = sum % 11;
        return (remainder < 2) ? 0 : 11 - remainder;
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        CNPJService service = new CNPJService();

        var cnpj = "12ABC34501DE"; // Exemplo de CNPJ alfanumérico
        try
        {
            var result = await service.ConsultarCNPJ(cnpj);
            Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
```