using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGL.Dto
{
    public class Pet
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public PetType Type { get; set; }
    }
}
