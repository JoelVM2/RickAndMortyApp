using Microsoft.AspNetCore.Mvc;
using RickAndMortyApp.Services;

namespace RickAndMortyAPI.Controllers
{
    public class CharactersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var service = new CharacterService();
            var characters = await service.GetAllCharacters();

            return View(characters);
        }
    }
}
