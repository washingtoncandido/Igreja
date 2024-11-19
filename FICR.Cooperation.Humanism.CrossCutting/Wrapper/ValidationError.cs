﻿using Newtonsoft.Json;


namespace FICR.Cooperation.Humanism.CrossCutting.Wrapper
{
    public class ValidationError(string? field, string message)
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Field { get; } = field != string.Empty ? field : null;

        public string Message { get; } = message;
    }
}
