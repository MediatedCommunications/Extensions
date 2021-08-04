using System.Threading.Tasks;

namespace System.Events.Async {
    public delegate Task AsyncDelegate<TSender, TArgs>(TSender sender, TArgs args);
}
