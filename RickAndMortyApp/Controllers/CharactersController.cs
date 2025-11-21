using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RickAndMortyApp.Models;

namespace RickAndMortyApp.Controllers
{
    public class CharactersController : Controller
    {
        private readonly HttpClient _http = new HttpClient();
        private const string BaseUrl = "https://rickandmortyapi.com/api/character";


        // Obtiene los personajes de la API de Rick and Morty segun los filtros especificados
        public async Task<IActionResult> Index(string gender, string species, string status, string name, int page = 1)
        {
            try
            {
                // Construye la query según los filtros seleccionados
                var queryParams = new List<string>();
                if (!string.IsNullOrEmpty(status)) queryParams.Add($"status={Uri.EscapeDataString(status)}");
                if (!string.IsNullOrEmpty(name)) queryParams.Add($"name={Uri.EscapeDataString(name)}");
                if (!string.IsNullOrEmpty(species)) queryParams.Add($"species={Uri.EscapeDataString(species)}");
                if (!string.IsNullOrEmpty(gender)) queryParams.Add($"gender={Uri.EscapeDataString(gender)}");
                queryParams.Add($"page={page}");

                string url = BaseUrl + "?" + string.Join("&", queryParams);

                // Llamada a la API
                var response = await _http.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return View(new CharacterListViewModel());
                }

                string json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

                if (apiResponse?.Results == null || apiResponse.Info == null)
                {
                    return View(new CharacterListViewModel());
                }

                // Ajustar la página si excede el total de páginas
                int totalPages = apiResponse.Info.Pages;
                if (page > totalPages) page = totalPages;

                // Construir el modelo para la vista
                var model = new CharacterListViewModel
                {
                    Characters = apiResponse.Results,
                    Status = status ?? "",
                    Species = species ?? "",
                    Gender = gender ?? "",
                    Name = name ?? "",
                    Page = page,
                    TotalPages = totalPages
                };

                return View(model);
            }
            catch (HttpRequestException)
            {
                // Error de conexión
                return View(new CharacterListViewModel());
            }
            catch (JsonException)
            {
                // Error al procesar la respuesta de la API
                return View(new CharacterListViewModel());
            }
            catch (Exception)
            {
                return View(new CharacterListViewModel());
            }
        }

    }
}
