using System;

namespace ModularArchitecture.Shared.DTOs.Connector
{
    /// <summary>
    /// ConnectorResponse
    /// </summary>
    public class ConnectorResponse
    {
        /// <summary>
        /// Connector ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Connector Name
        /// </summary>
        public string Name { get; set; }
    }
}
