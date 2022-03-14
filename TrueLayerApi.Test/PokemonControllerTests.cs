using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TrueLayerApi.Controllers;
using TrueLayerApi.Models;
using TrueLayerApi.Services;
using Xunit;

namespace TrueLayerApi.Test
{
    public class PokemonControllerTests
    {
        [Fact]
        public async Task GetTranslationAsync_TeststringEmopty_ReturnBadRequest()
        {
            var pokemonServiceStub = new Mock<IPokemonService>();
            pokemonServiceStub.Setup(x => x.GetCachedResponse(It.IsAny<string>()))
                .ReturnsAsync((PokemonResponseDto)null);

            var controller = new PokemonController(pokemonServiceStub.Object);

            var result = await controller.GetTranslationAsync(string.Empty);
            Assert.IsType<BadRequestResult>(result);
        } 
        
        [Fact]
        public async Task GetTranslationAsync_OnSuccess_Returns200()
        {
            var testString = "Test";
            var pokemonServiceStub = new Mock<IPokemonService>();

            var controller = new PokemonController(pokemonServiceStub.Object);

            var result = await controller.GetTranslationAsync(testString) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
