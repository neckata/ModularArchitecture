using System.Collections.Generic;

namespace ModularArchitecture.Shared.Infrastructure.Utilities
{
    public sealed class ModuleTypes
    {
        private ModuleTypes() { }

        public List<string> Modules { get; set; }

        private static ModuleTypes instance = null;

        public static ModuleTypes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModuleTypes();
                }
                return instance;
            }
        }
    }
}
