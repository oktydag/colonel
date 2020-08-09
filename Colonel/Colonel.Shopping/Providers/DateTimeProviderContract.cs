using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Colonel.Shopping.Providers
{
    public class DateTimeProviderContract
    {
        private static ThreadLocal<Stack<DateTimeProviderContract>> ThreadScopeStack = 
            new ThreadLocal<Stack<DateTimeProviderContract>>(() => new Stack<DateTimeProviderContract>());

        public DateTime ContextDateTimeUtcNow;
        private Stack<DateTimeProviderContract> _contextStack = new Stack<DateTimeProviderContract>();

        public DateTimeProviderContract(DateTime contextDateTimeUtcNow)
        {
            ContextDateTimeUtcNow = contextDateTimeUtcNow;
            ThreadScopeStack.Value.Push(this);
        }

        public static DateTimeProviderContract Current
        {
            get
            {
                if (ThreadScopeStack.Value.Count == 0)
                {
                    return null;
                }
                return ThreadScopeStack.Value.Peek();
            }
        }
    }
}
