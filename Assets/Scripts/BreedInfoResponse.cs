using System;
using Newtonsoft.Json;

[Serializable]
public class BreedInfoResponse
{
    [JsonProperty("data")]
    public BreedData data { get; set; }

    [JsonProperty("links")]
    public Links links { get; set; }
}

[Serializable]
public class BreedData
{
    [JsonProperty("id")]
    public string id { get; set; }

    [JsonProperty("type")]
    public string type { get; set; }

    [JsonProperty("attributes")]
    public Attributes attributes { get; set; }

    [JsonProperty("relationships")]
    public Relationships relationships { get; set; }
}

[Serializable]
public class Attributes
{
    [JsonProperty("name")]
    public string name { get; set; }

    [JsonProperty("description")]
    public string description { get; set; }

    [JsonProperty("life")]
    public Life life { get; set; }

    [JsonProperty("male_weight")]
    public Weight male_weight { get; set; }

    [JsonProperty("female_weight")]
    public Weight female_weight { get; set; }

    [JsonProperty("hypoallergenic")]
    public bool hypoallergenic { get; set; }
}

[Serializable]
public class Life
{
    [JsonProperty("max")]
    public int max { get; set; }

    [JsonProperty("min")]
    public int min { get; set; }
}

[Serializable]
public class Weight
{
    [JsonProperty("max")]
    public int max { get; set; }

    [JsonProperty("min")]
    public int min { get; set; }
}

[Serializable]
public class Relationships
{
    [JsonProperty("group")]
    public Group group { get; set; }
}

[Serializable]
public class Group
{
    [JsonProperty("data")]
    public GroupData data { get; set; }
}

[Serializable]
public class GroupData
{
    [JsonProperty("id")]
    public string id { get; set; }

    [JsonProperty("type")]
    public string type { get; set; }
}

[Serializable]
public class Links
{
    [JsonProperty("self")]
    public string self { get; set; }
}