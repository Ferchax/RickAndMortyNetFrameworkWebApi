using RickAndMorty3.Dtos;
using RickAndMorty3.Models;
using RickAndMorty3.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace RickAndMorty3.Controllers
{
    [RoutePrefix("api/rickandmorty")]
    public class RickAndMortyController : ApiController
    {
        private readonly ICharacterService _rickAndMortyService;

        public RickAndMortyController(ICharacterService rickAndMortyService)
        {
            _rickAndMortyService = rickAndMortyService;
        }

        [Route("character")]
        [HttpGet]
        public async Task<IHttpActionResult> Character(string name)
        {
            var responseCharacterDto = await _rickAndMortyService.GetCharactersAsync();

            if (responseCharacterDto == null) return NotFound();

            var character = SearchByName(responseCharacterDto, name);

            if (character == null) return NotFound();

            return Ok(character);
        }

        private CharacterModel SearchByName(ResponseCharacterDto responseCharacterDto, string name)
        {
            var character = responseCharacterDto.Results.FirstOrDefault(c =>
                c.Name.Trim().ToLower() == name.Trim().ToLower());

            return character;
        }
    }    
}
