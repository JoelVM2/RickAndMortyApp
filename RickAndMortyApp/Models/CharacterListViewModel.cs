using RickAndMortyApp.Models;

namespace RickAndMortyApp.Models
{
    public class CharacterListViewModel
    {
        public List<Character> Characters { get; set; } = new List<Character>();

        // Filtros
        public string Status { get; set; } = "";
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public string Gender { get; set; } = "";
        // Paginación
        public int Page { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
    }
}
