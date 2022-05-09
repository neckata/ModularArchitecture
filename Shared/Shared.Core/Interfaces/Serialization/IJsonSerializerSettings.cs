using Newtonsoft.Json;

namespace ModularArchitecture.Shared.Core.Interfaces.Serialization
{
    public interface IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; }
    }
}