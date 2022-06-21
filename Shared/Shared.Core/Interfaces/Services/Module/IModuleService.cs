using ModularArchitecture.Shared.Core.Wrapper;
using ModularArchitecture.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularArchitecture.Shared.Core.Interfaces.Services.Module
{
    /// <summary>
    /// Get modules which are available for use
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// Gets all available modules
        /// </summary>
        /// <returns></returns>
        Task<IResult<List<ModuleResponse>>> GetAllModulesAsync();

        /// <summary>
        /// Gets Module detailed information
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        Task<IResult<ModuleResponse>> GetModuleAsync(Guid ModuleId);
    }
}
