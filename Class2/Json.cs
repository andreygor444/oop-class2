namespace oop2_2023_class1;

class Json
{
    private class JsonException : Exception
    {
        public JsonException(string message) : base(message)
        {
        }
    }

    protected enum ValueType
    {
        kValue,
        kObject,
        kArray
    }

    protected ValueType type;

    public bool IsValue() => type == ValueType.kValue;
    public bool IsObject() => type == ValueType.kObject;
    public bool IsArray() => type == ValueType.kArray;

    public virtual int GetInt()
    {
        throw new JsonException("I have not value");
    }

    public virtual Json this[int i]
    {
        get
        {
            if (IsArray()) return ((JsonArray)this)[i];
            throw new JsonException("Invalid operation: []");
        }
    }

    public virtual Json this[string i]
    {
        get
        {
            if (IsObject()) return ((JsonObject)this)[i];
            throw new JsonException("Invalid operation: []");
        }
    }

    public static Json FromString(string str)
    {
        JsonArray ParseArray(string strJson)
        {
            var builder = new JsonArrayBuilder();
            strJson = strJson.Trim();
            if (strJson[0] != '[' || strJson[^1] != ']')
                throw new JsonException("Wrong format");

            foreach (var item in strJson.Substring(1, strJson.Length - 2).Split(','))
            {
                if (int.TryParse(item, out var val))
                {
                    builder.Add(val);
                }
                else
                {
                    builder.Add(ParseObject(item.Trim()));
                }
            }

            return (JsonArray)builder;
        }

        JsonObject ParseObject(string strJson)
        {
            var builder = new JsonObjectBuilder();
            strJson = strJson.Trim();
            if (strJson[0] != '{' || strJson[^1] != '}')
                throw new JsonException("Wrong format");

            for (int i = 1; i < strJson.Length; ++i)
            {
                if (strJson[i] == '"')
                {
                    int j = i + 1;
                    while (j < strJson.Length && strJson[j] != '"') ++j;
                    if (j == strJson.Length) throw new JsonException("Wrong format");
                    string key = strJson.Substring(i + 1, j - i - 1);
                    i = j + 1;
                    while (i < strJson.Length && strJson[i] != ':') ++i;
                    if (i == strJson.Length) throw new JsonException("Wrong format");
                    while (i < strJson.Length && strJson[i] != '{' && strJson[i] != '[' &&
                           !char.IsDigit(strJson[i])) ++i;
                    if (i == strJson.Length) throw new JsonException("Wrong format");
                    j = i + 1;
                    if (strJson[i] == '{' || strJson[i] == '[')
                    {
                        var brackets = strJson[i] == '{' ? "{}" : "[]";
                        int openedBracketsCnt = 1, closedBracketsCnt = 0;
                        while (j < strJson.Length - 1 && openedBracketsCnt != closedBracketsCnt)
                        {
                            if (strJson[j] == brackets[0]) ++openedBracketsCnt;
                            else if (strJson[j] == brackets[1]) ++closedBracketsCnt;
                            ++j;
                        }

                        if (openedBracketsCnt != closedBracketsCnt) throw new JsonException("Wrong format");
                        string subStr = strJson.Substring(i, j - i);
                        Json value = strJson[i] == '{' ? ParseObject(subStr) : ParseArray(subStr);
                        builder.Add(key, value);
                    }
                    else
                    {
                        while (j < strJson.Length && char.IsDigit(strJson[j])) ++j;
                        int value = int.Parse(strJson.Substring(i, j - i));
                        builder.Add(key, value);
                    }

                    i = j - 1;
                }
            }

            return (JsonObject)builder;
        }

        return ParseObject(str);
    }

    public static string ToString(Json json)
    {
        return json.ToString();
    }
}

class JsonValue : Json
{
    private readonly int value;

    public JsonValue(int value)
    {
        type = ValueType.kValue;
        this.value = value;
    }

    public int GetValue() => value;

    public override int GetInt() => value;
    public override string ToString() => value.ToString();
}

class JsonArray : Json
{
    private readonly List<Json> array;

    public JsonArray(List<Json> array)
    {
        type = ValueType.kArray;
        this.array = array;
    }

    public Json this[int i] => array[i];
    public int Size() => array.Count;

    public override string ToString()
    {
        string text = "[";
        for (int index = 0; index < array.Count; index++)
        {
            text += array[index].ToString();
            if (index < array.Count - 1) text += ",";
        }

        text += "]";
        return text;
    }
}

class JsonObject : Json
{
    private Dictionary<string, Json> dictionary;

    public JsonObject(Dictionary<string, Json> dictionary)
    {
        type = ValueType.kObject;
        this.dictionary = dictionary;
    }

    public bool HasValue(string key) => dictionary.ContainsKey(key);
    public Json this[string s] => dictionary[s];
    public int Size() => dictionary.Count;

    public override string ToString()
    {
        string text = "{";

        string last = dictionary.Keys.Last();
        foreach (var pair in dictionary)
        {
            text += "\"" + pair.Key + "\":" + pair.Value.ToString();
            if (pair.Key != last) text += ",";
        }

        text += "}";
        return text;
    }
}

class JsonObjectBuilder
{
    private Dictionary<string, Json> dict;

    public JsonObjectBuilder()
    {
        this.dict = new Dictionary<string, Json>();
    }

    public JsonObjectBuilder Add(string key, Json value)
    {
        dict.Add(key, value);
        return this;
    }

    public JsonObjectBuilder Add(string key, int value)
    {
        dict.Add(key, new JsonValue(value));
        return this;
    }

    public static explicit operator Json(JsonObjectBuilder jab) => new JsonObject(jab.dict);
}

class JsonArrayBuilder
{
    private List<Json> array;

    public JsonArrayBuilder()
    {
        this.array = new List<Json>();
    }

    public JsonArrayBuilder Add(Json value)
    {
        array.Add(value);
        return this;
    }

    public JsonArrayBuilder Add(int value)
    {
        array.Add(new JsonValue(value));
        return this;
    }

    public static explicit operator Json(JsonArrayBuilder jab) => new JsonArray(jab.array);
}