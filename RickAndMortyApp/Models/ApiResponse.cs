using System.Collections.Generic;
using RickAndMortyApp.Models;

namespace RickAndMortyApp.Models
{
    public class ApiResponse
    {
        public ApiInfo Info { get; set; }
        public List<Character> Results { get; set; }
    }
}
