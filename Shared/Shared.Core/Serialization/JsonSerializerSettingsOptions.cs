using ModularArchitecture.Shared.Core.Interfaces.Serialization;
using Newtonsoft.Json;
using System.Text.Json;

namespace ModularArchitecture.Shared.Core.Serialization
{
    public class JsonSerializerSettingsOptions : IJsonSerializerSettingsOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();

        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}