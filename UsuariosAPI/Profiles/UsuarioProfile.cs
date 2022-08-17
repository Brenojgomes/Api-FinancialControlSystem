using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;
using AutoMapper;

namespace UsuariosAPI.Profiles
{
    public class UsuarioProfile : Profile
    {

        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();

        }
    }
}
