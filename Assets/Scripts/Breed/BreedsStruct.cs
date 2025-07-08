using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DefaultNamespace
{
    [Serializable]
    public class BreedsResponse
    {
        [JsonProperty("data")]
        public List<BreedData> Data { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }
    }

    [Serializable]
    public class BreedData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; }
    }

    [Serializable]
    public class Attributes
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("life")]
        public Life Life { get; set; }

        [JsonProperty("male_weight")]
        public Weight MaleWeight { get; set; }

        [JsonProperty("female_weight")]
        public Weight FemaleWeight { get; set; }

        [JsonProperty("hypoallergenic")]
        public bool Hypoallergenic { get; set; }
    }

    [Serializable]
    public class Life
    {
        [JsonProperty("max")]
        public int Max { get; set; }

        [JsonProperty("min")]
        public int Min { get; set; }
    }

    [Serializable]
    public class Weight
    {
        [JsonProperty("max")]
        public int Max { get; set; }

        [JsonProperty("min")]
        public int Min { get; set; }
    }

    [Serializable]
    public class Relationships
    {
        [JsonProperty("group")]
        public Group Group { get; set; }
    }

    [Serializable]
    public class Group
    {
        [JsonProperty("data")]
        public GroupData Data { get; set; }
    }

    [Serializable]
    public class GroupData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    [Serializable]
    public class Links
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("current")]
        public string Current { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }
}