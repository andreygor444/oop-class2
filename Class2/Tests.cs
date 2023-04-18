using Newtonsoft.Json;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace oop2_2023_class1;

public class Tests
{
    [Test]
    public void ReadWriteTest()
    {
        string str1 = "{\"v\":1,\"a\":[1,1]}";
        string expected1 = "{\"v\":1,\"a\":[1,1]}";
        string str2 = "{\"glossary\":{\"title\": 123,\"GlossDiv\": {            \"title\": 1,\"GlossList\": {                \"GlossEntry\": {                    \"ID\": 0,\"SortAs\": 2,\"GlossTerm\": 3,\"Acronym\": 4,\"Abbrev\": 5,\"GlossDef\": {                        \"para\": 8888,\"GlossSeeAlso\": [1, 2]                    },\"GlossSee\": -100                }            }        }    }}";
        string expected2 = "{\"glossary\":{\"title\":123,\"GlossDiv\":{\"title\":1,\"GlossList\":{\"GlossEntry\":{\"ID\":0,\"SortAs\":2,\"GlossTerm\":3,\"Acronym\":4,\"Abbrev\":5,\"GlossDef\":{\"para\":8888,\"GlossSeeAlso\":[1,2]},\"GlossSee\":-100}}}}}";
        string bad1 = "bad";
        string bad2 = "{\"v\":1,\"a\":[1,j]}";
        string bad3 = "{\"v\",\"a\":[1,1]}";


        That(Json.FromString(str1).ToString(), Is.EqualTo(expected1));
        That(Json.FromString(str2).ToString(), Is.EqualTo(expected2));
        Throws<Json.JsonException>(() => Json.FromString(bad1));
        Throws<Json.JsonException>(() => Json.FromString(bad2));
        Throws<Json.JsonException>(() => Json.FromString(bad3));
    }

    [Test]
    public void ChecksTest()
    {
        Json json = Json.FromString("{\"v\":1,\"a\":[1,1]}");
        That(json["v"].IsArray(), Is.EqualTo(false));
        That(json["v"].IsObject(), Is.EqualTo(false));
        That(json["v"].IsValue(), Is.EqualTo(true));
        
        That(json["a"].IsObject(), Is.EqualTo(false));
        That(json["a"].IsValue(), Is.EqualTo(false));
        That(json["a"].IsArray(), Is.EqualTo(true));
        
        That(json["a"][0].IsArray(), Is.EqualTo(false));
        That(json["a"][0].IsObject(), Is.EqualTo(false));
        That(json["a"][0].IsValue(), Is.EqualTo(true));
    }

    [Test]
    public void GetIntTest()
    {
        Json json = Json.FromString("{\"v\":1,\"a\":[1,2]}");
        That(json["v"].GetInt(), Is.EqualTo(1));
        That(json["a"][0].GetInt(), Is.EqualTo(1));
        That(json["a"][1].GetInt(), Is.EqualTo(2));
        
        Throws<Json.JsonException>(() => json["a"].GetInt());
        Throws<Json.JsonException>(() => json.GetInt());
    }

    [Test]
    public void GetElementByIndexTest()
    {
        Json json = Json.FromString("{\"v\":1,\"a\":[1,2]}");
        That(json["v"].ToString(), Is.EqualTo("1"));
        That(json["a"].ToString(), Is.EqualTo("[1,2]"));
        That(json["a"][0].ToString(), Is.EqualTo("1"));
        That(json["a"][1].ToString(), Is.EqualTo("2"));
    }
}
