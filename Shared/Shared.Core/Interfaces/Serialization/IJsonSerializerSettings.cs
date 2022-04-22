using Newtonsoft.Json;

namespace Gamification.Shared.Core.Interfaces.Serialization
{
    public interface IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; }
    }
}