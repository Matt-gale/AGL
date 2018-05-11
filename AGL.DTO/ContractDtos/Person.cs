using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AGL.Dto
{
    public class Person
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public GenderType Gender { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("pets")]
        public IList<Pet> Pets { get; set; }
    }
}
