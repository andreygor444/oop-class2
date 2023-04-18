using System;
using System.Collections.Generic;
using System.Linq;


namespace oop2_2023_class1
{
    internal class Program
    {

        class Json
        {
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

            public static Json FromString(string strJson)
            {
                return new Json();
                // TODO
            }

            public static string ToString(Json json)
            {
                return json.ToString();
            }
        }

        class JsonValue:Json
        {
            private int value;

            public JsonValue()
            {
                this.type = ValueType.kValue;
            }
            public int GetValue() => value;
            public void SetValue(int value) => this.value = value;
            public override string ToString() => value.ToString();
        }

        class JsonArray:Json
        {
            private List<Json> array;

            public JsonArray()
            {
                this.type = ValueType.kArray;
            }
            public Json GetValue(int index) => array[index];
            public int Size() => array.Count;
            public void SetArray(List<Json> array) => this.array = array;
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

        class JsonObject: Json
        {
            private Dictionary<string, Json> dictonary;

            public JsonObject()
            {
                this.type = ValueType.kObject;
            }

            public Json GetValue(string key) => dictonary[key];
            public bool HasValue(string key) => dictonary.ContainsKey(key);
            public int Size() => dictonary.Count;
            public void SetObject(Dictionary<string, Json> dictonary) => this.dictonary = dictonary;
            public override string ToString()
            {
                string text = "{";

                string last = dictonary.Keys.Last();
                foreach (var pair in dictonary)
                {
                    text += "\"" + pair.Key + "\":"+pair.Value.ToString();
                    if (pair.Key != last) text += ",";
                }
                
                text += "}";
                return text;
            }
        }
        
        public static void Main(string[] args)
        {
            JsonValue v1 = new JsonValue();
            v1.SetValue(1);
            
            JsonValue v2 = new JsonValue();
            v2.SetValue(1);
            JsonValue v3 = new JsonValue();
            v3.SetValue(1);

            JsonArray a = new JsonArray();
            List<Json> list = new List<Json>();
            list.Add(v2);
            list.Add(v3);
            
            a.SetArray(list);

            JsonObject o = new JsonObject();
            Dictionary<string, Json> dictionary = new Dictionary<string, Json>();
            dictionary.Add("v",v1);
            dictionary.Add("a",a);
            o.SetObject(dictionary);
            
            Console.WriteLine(Json.ToString(o));
        }
    }
}