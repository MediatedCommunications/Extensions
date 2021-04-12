namespace System.Text.Json {
    public class LowerCaseNamingPolicy : JsonNamingPolicy {

        public static JsonNamingPolicy Instance { get; } = new LowerCaseNamingPolicy();

        public override string ConvertName(string name) {
            var ret = name.ToLower();
            return ret;
        }
    }
}
