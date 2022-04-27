using System.Text.Json;
using Gamification.Shared.Core.Interfaces.Serialization;
using Newtonsoft.Json;

namespace Gamification.Shared.Core.Serialization
{
    public class JsonSerializerSettingsOptions : IJsonSerializerSettingsOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();

        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}