namespace HarmonyLib {
    public abstract class Always<TValue, TThis> : FuncHandler<TValue, TThis> {
        
        private TValue Value { get; }
        public Always(TValue Value) {
            this.Value = Value;
        }

        protected override TValue Invoke() {
            return Value;
        }

    }

}
