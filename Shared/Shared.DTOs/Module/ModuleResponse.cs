using System;

namespace ModularArchitecture.Shared.DTOs.Module
{
    /// <summary>
    /// ModuleResponse
    /// </summary>
    public class ModuleResponse
    {
        /// <summary>
        /// Module ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Module Name
        /// </summary>
        public string Name { get; set; }
    }
}
