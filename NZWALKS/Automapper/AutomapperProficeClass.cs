using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using NZWALKS.Model.Domine;
using NZWALKS.DTO;
namespace NZWALKS.Automapper
{
    public class AutomapperProficeClass:Profile
    {
        public AutomapperProficeClass() 
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddWalkClass,Walk>().ReverseMap();
            CreateMap<Walk,ReturnWalkDtoClass>().ReverseMap();
        }

    }
}
