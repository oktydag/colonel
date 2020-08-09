using System;

namespace Colonel.Shopping.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetUtcNow();
    }
}
