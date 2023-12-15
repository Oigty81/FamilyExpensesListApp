namespace Backend.Dtos
{
    using Newtonsoft.Json;
    using System;

    [Serializable]
    public class TokenDto
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
