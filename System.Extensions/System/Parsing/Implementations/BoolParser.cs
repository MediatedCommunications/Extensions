namespace System {
    public record BoolParser : StructParser<bool> {

        public override bool TryGetValue(string? Input, out bool Result) {
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
