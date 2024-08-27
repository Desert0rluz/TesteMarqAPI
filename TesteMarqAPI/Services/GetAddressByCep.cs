using Models;
using Newtonsoft.Json;

namespace Services;

public class GetAddressByCep
{
    private readonly HttpClient httpClient;

    public GetAddressByCep()
    {
        httpClient = new HttpClient();
    }

    public async Task<Address> ObterEnderecoPorCepAsync(string cep)
    {
        string url = $"https://viacep.com.br/ws/{cep}/json/";
        HttpResponseMessage response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        Address endereco = JsonConvert.DeserializeObject<Address>(responseBody);

        return endereco;
    }
}
