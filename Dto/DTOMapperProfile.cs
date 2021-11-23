using AutoMapper;
using MimicApp_Api.Helpers;
using MimicApp_Api.Models;

namespace MimicApp_Api.Dto
{
    public class DTOMapperProfile: Profile
    {
        public DTOMapperProfile()
        {
            CreateMap<Palavra, PalavraDTO>();
            CreateMap<PaginationList<Palavra>,PaginationList<PalavraDTO>>();
        }
    }
}