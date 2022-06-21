using ModularArchitecture.Shared.Core.Domain;

namespace ModularArchitecture.Shared.Core.Entities
{
    public class Action : BaseEntity
    {
        public string ModuleType { get; set; }

        public Action()
        {

        }
    }
}
