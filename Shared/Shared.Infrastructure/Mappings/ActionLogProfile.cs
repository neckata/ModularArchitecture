using AutoMapper;
using Gamification.DTOs.Actions;
using Gamification.Shared.Core.Entities;

namespace Gamification.Shared.Infrastructure.Mappings
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