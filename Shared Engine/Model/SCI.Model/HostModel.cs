using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Engine.Model
{

    public class ExhangeRateModel
    {
        //[Required]
        [JsonPropertyName("Value")]
        public string Value { get; set; }
        [Key]
        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [Key]
        [JsonPropertyName("Target")]
        public string Target { get; set; }

        [Key]
        [JsonPropertyName("TargetValue")]
        public string TargetValue { get; set; }
    }

    public class ExchangeRateRequestModel
    {
        [JsonPropertyName("SourceValue")]
        public string SourceValue { get; set; }

        [JsonPropertyName("Source")]
        public string Source { get; set; }

        [Key]
        [JsonPropertyName("Target")]
        public string Target { get; set; }
    }
}