using RickAndMorty3.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty3.Services
{
    public interface ICharacterService
    {
        public Task<ResponseCharacterDto> GetCharactersAsync();
    }
}
