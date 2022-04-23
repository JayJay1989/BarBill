using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BarBill
{
    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> pFactory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(pFactory));
        }

        public AsyncLazy(Func<Task<T>> pFactory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(pFactory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
}
