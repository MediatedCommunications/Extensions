namespace System {
    public record EnumValueParser<T> : StructParser<T> where T : struct {
        public EnumValueParser(string? Value) : base(Value) {
        }

        public override bool TryGetValue(out T Result) {
            return Enum.TryParse(Value, true, out Result);
        }
    }


}
