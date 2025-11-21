using Newtonsoft.Json;
using RickAndMortyApp.Models;

namespace RickAndMortyApp.Services
{
    public class CharacterService
    {
        private readonly HttpClient _http;

        public CharacterService()
        {
            _http = new HttpClient();
        }

        // Obtiene todos los personajes de la API de Rick and Morty
        public async Task<List<Character>> GetAllCharacters()
        {
            var characters = new List<Character>();
            string baseUrl = "https://rickandmortyapi.com/api/character";
            int currentPage = 1;

            try
            {
                while (true)
                {
                    string url = $"{baseUrl}/?page={currentPage}";

                    var response = await _http.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Error al obtener datos de la API: {response.StatusCode}");
                    }

                    var json = await response.Content.ReadAsStringAsync();

                    var api = JsonConvert.DeserializeObject<ApiResponse>(json);
                    if (api?.Results == null)
                    {
                        break;
                    }

                    characters.AddRange(api.Results);

                    if (api.Info?.Next == null)
                        break;

                    currentPage++;
                }
            }
            catch (HttpRequestException ex)
            {
                // Error de red
                Console.WriteLine($"Error de conexión: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Error de deserialización
                Console.WriteLine($"Error al procesar los datos de la API: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }

            return characters;
        }

    }
}
