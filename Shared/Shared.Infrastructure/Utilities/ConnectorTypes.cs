using System.Collections.Generic;

namespace ModularArchitecture.Shared.Infrastructure.Utilities
{
    public sealed class ConnectorTypes
    {
        private ConnectorTypes() { }

        public List<string> Modules { get; set; }

        private static ConnectorTypes instance = null;

        public static ConnectorTypes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectorTypes();
                }
                return instance;
            }
        }
    }
}
