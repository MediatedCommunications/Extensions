namespace System {
    public record EnumStructParser<T> : StructParser<T> where T : struct {

        public override bool TryGetValue(out T Result) {
            return Enum.TryParse(Input, true, out Result);
        }
    }


}
