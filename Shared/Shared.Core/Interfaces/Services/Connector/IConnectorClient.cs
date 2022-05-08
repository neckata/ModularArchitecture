using Gamification.Shared.Core.Wrapper;
using System.Threading.Tasks;
using Gamification.DTOs.Actions;
using System.Collections.Generic;
using Gamification.Shared.Core.Entities;

namespace Gamification.Shared.Core.Interfaces.Services.Connector
{
    public interface IConnectorClient
    {
        Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request);

        Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request);

        Task<IResult<List<Action>>> GetActions();
    }
}
