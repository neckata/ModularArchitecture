using AutoMapper;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Entities;

namespace ModularArchitecture.Shared.Infrastructure.Mappings
{
    public class ActionLogProfile : Profile
    {
        public ActionLogProfile()
        {
            CreateMap<UpdateActionRequest, Action>().ReverseMap();

            CreateMap<CreateActionRequest, Action>().ReverseMap();
        }
    }
}