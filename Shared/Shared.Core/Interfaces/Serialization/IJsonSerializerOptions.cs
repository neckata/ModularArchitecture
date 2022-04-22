using System.Text.Json;

namespace Gamification.Shared.Core.Interfaces.Serialization
{
    public interface IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; }
    }
}