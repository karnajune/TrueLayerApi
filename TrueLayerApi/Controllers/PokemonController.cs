using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueLayerApi.Models;
using TrueLayerApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrueLayerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("{testWord}")]
        public async Task<IActionResult> GetTranslationAsync(string testWord)
        {
            if (string.IsNullOrEmpty(testWord)) return BadRequest();
            try
            {
                var result = await _pokemonService.GetCachedResponse(testWord);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(detail: "Could not serve your request at this moment.Please contact admin.", statusCode:500);
            }
        }
    }
}
