namespace System {
    public record BoolValueParser : StructParser<bool> {
        public BoolValueParser(string? Value) : base(Value) {
        
        }

        public override bool TryGetValue(out bool Result) {
            var ret = false;
            Result = false;

            {
                if (ret == false && bool.TryParse(Input, out var V1)) {
                    ret = true;
                    Result = V1;
                }
            }

            {
                if (ret == false && long.TryParse(Input, out var V1)) {
                    ret = true;
                    Result = V1 != 0;
                }
            }

            {
                if (ret == false && double.TryParse(Input, out var V1)) {
                    ret = true;
                    Result = V1 != 0;
                }
            }

            return ret;
        }
    }


}
