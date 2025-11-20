using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RickAndMortyApp.Models;

namespace RickAndMortyApp.Controllers
{
    public class CharactersController : Controller
    {
        private readonly HttpClient _http = new HttpClient();
        private const string BaseUrl = "https://rickandmortyapi.com/api/character";

        public async Task<IActionResult> Index(string gender, string species,string status, string name, int page = 1)
        {
            try
            {
                // Construir query
                var queryParams = new List<string>();
                if (!string.IsNullOrEmpty(status)) queryParams.Add($"status={status}");
                if (!string.IsNullOrEmpty(name)) queryParams.Add($"name={name}");
                if(!string.IsNullOrEmpty(species)) queryParams.Add($"species={species}");
                if (!string.IsNullOrEmpty(gender)) queryParams.Add($"gender={gender}");
                queryParams.Add($"page={page}");

                string url = BaseUrl + "?" + string.Join("&", queryParams);

                string json = await _http.GetStringAsync(url);
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

                var model = new CharacterListViewModel
                {
                    Characters = apiResponse.Results,
                    Status = status ?? "",
                    Species = species ?? "",
                    Gender = gender ?? "",
                    Name = name ?? "",
                    Page = page,
                    TotalPages = apiResponse.Info.Pages
                };

                return View(model);
            }
            catch (HttpRequestException)
            {
                // Error si no encuentra resultados
                return View(new CharacterListViewModel());
            }
        }
    }
}
