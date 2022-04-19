using System.Text.Json;
using Shared.Core.Interfaces.Serialization;
using Newtonsoft.Json;

namespace Shared.Core.Serialization
{
    public class JsonSerializerSettingsOptions : IJsonSerializerSettingsOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();

        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}
