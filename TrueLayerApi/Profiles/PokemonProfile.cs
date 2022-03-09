using AutoMapper;
using TrueLayerApi.Models;

namespace TrueLayerApi.Profiles
{
    public class PokemonProfile:Profile
    {
        public PokemonProfile()
        {
            CreateMap<ShakespeareResponse, PokemonResponseDto>()                
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Contents == null ? string.Empty : src.Contents.Text))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Contents == null ? string.Empty : src.Contents.Translated))
                 .ForMember(dest => dest.Errormessage, option => option.Ignore())
                .AfterMap((src, dst) => {
                 if (src.Error != null)
                 {
                     dst.Errormessage = "Not able to server your request, Please contact Admin";
                 }
                 });
            //.ForMember(dest => dest.Errormessage, opt =>opt.Er opt.MapFrom(src => src.Error == null ? src.Error.Ignore : "Not able to server your request, Please contact Admin"));

        }
    }
}
