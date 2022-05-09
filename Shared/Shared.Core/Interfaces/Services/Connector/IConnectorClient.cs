using ModularArchitecture.Shared.Core.Wrapper;
using System.Threading.Tasks;
using ModularArchitecture.DTOs.Actions;
using System.Collections.Generic;
using ModularArchitecture.Shared.Core.Entities;

namespace ModularArchitecture.Shared.Core.Interfaces.Services.Connector
{
    public interface IConnectorClient
    {
        Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request);

        Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request);

        Task<IResult<List<Action>>> GetActions();
    }
}
