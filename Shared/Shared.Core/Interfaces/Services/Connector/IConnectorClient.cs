using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.Interfaces.Services.Connector
{
    public interface IConnectorClient
    {
        Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request);

        Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request);

        Task<IResult<List<Action>>> GetActionsAsync();
    }
}
