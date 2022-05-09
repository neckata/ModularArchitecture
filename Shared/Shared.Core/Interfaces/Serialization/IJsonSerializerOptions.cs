using System.Text.Json;

namespace ModularArchitecture.Shared.Core.Interfaces.Serialization
{
    public interface IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; }
    }
}