using System;

namespace Colonel.Shopping.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private static Lazy<DateTimeProvider> _lazyInstance = new Lazy<DateTimeProvider>(() => new DateTimeProvider());
        private DateTimeProvider()
        {
        }

        public static DateTimeProvider Instance
        {
            get
            {
                return _lazyInstance.Value;
            }
        }

        private Func<DateTime> _defaultCurrentFunction = () => DateTime.UtcNow;

        public DateTime GetUtcNow()
        {
            if (DateTimeProviderContract.Current == null)
            {
                return _defaultCurrentFunction.Invoke();
            }
            else
            {
                return DateTimeProviderContract.Current.ContextDateTimeUtcNow;
            }
        }
    }
}
