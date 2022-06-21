using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.Interfaces.Services.Action
{
    /// <summary>
    /// Base interface to be implemented by all Modules
    /// </summary>
    public interface IActionService
    {
        /// <summary>
        /// Update action in Module
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request);

        /// <summary>
        /// Create action in Module
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request);

        /// <summary>
        ///  Get created actions in Module
        /// </summary>
        /// <returns></returns>
        Task<IResult<List<Entities.Action>>> GetActionsAsync();
    }
}
