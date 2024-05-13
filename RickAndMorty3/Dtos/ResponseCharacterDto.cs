using RickAndMorty3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RickAndMorty3.Dtos
{
    public class ResponseCharacterDto
    {
        public PaginationModel Info { get; set; }
        public List<CharacterModel> Results { get; set; }
    }
}