﻿using Gamification.Shared.DTOs.Connector;
using Gamification.Shared.Core.Wrapper;
using System.Threading.Tasks;

namespace Gamification.Shared.Core.Interfaces.Services.Connector
{
    public interface IConnectorService
    {
        Task<IResult<string>> UpdateAsync(UpdateConnectorRequest request);
    }
}
