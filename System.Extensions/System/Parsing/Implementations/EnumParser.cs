namespace System {
    public record EnumParser<T> : StructParser<T> where T : struct {

        public override bool TryGetValue(string? Input, out T Result) {
            return Enum.TryParse(Input, true, out Result);
        }
    }


}
