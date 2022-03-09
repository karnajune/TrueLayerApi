using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueLayerApi.Models;

namespace TrueLayerApi.Services
{
   public interface IPokemonService
    {
        Task<PokemonResponseDto> GetCachedResponse(string testWord);
    }
}
