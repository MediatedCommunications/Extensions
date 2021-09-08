namespace System {
    public abstract class SpecialFolder {

        public abstract string GetPath();

        public string GetPath(params string[] SubFolders) {
            var ret = GetPath();
            foreach (var item in SubFolders) {
                ret = System.IO.Path.Combine(ret, item);
            }

            return ret;
        }

    }



}
