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

        public async Task<List<Character>> GetAllCharacters()
        {
            List<Character> characters = new();
            string baseUrl = "https://rickandmortyapi.com/api/character";
            int currentPage = 1;

            while (true)
            {
                string url = $"{baseUrl}/?page={currentPage}";
                var json = await _http.GetStringAsync(url);

                ApiResponse api = JsonConvert.DeserializeObject<ApiResponse>(json);

                characters.AddRange(api.Results);

                if (api.Info.Next == null)
                    break;

                currentPage++;
            }

            return characters;
        }
    }
}
