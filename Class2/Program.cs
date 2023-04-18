namespace oop2_2023_class1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            JsonValue v1 = new JsonValue(1);

            JsonValue v2 = new JsonValue(1);
            JsonValue v3 = new JsonValue(1);

            JsonArray a = (JsonArray) new JsonArrayBuilder().Add(v2).Add(v3);

            JsonObject o = (JsonObject) new JsonObjectBuilder().Add("v", v1).Add("a", a);
            
            Console.WriteLine(o["a"].ToString());
            Console.WriteLine(o["v"].ToString());
            Console.WriteLine(o["a"][0]);
            Console.WriteLine(o["a"][1]);

            Console.WriteLine(Json.ToString(o));
            
            Console.WriteLine(Json.FromString("{\"v\":1,\"a\":[1,1]}").ToString());
        }
    }
}