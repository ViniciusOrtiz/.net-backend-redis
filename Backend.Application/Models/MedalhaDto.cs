using System.Text.Json.Serialization;

namespace Backend.Application.Models
{
    public class MedalhaDto
    {
        [JsonPropertyName("pais")]
        public string Pais { get; set; } = string.Empty;
        [JsonPropertyName("ouro")]
        public int Ouro { get; set; } = 0;
        [JsonPropertyName("prata")]
        public int Prata { get; set; } = 0;
        [JsonPropertyName("bronze")]
        public int Bronze { get; set; } = 0;
    }
}
