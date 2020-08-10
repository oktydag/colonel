using Colonel.Shopping.Providers;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Colonel.Shopping.Tests
{
    [TestFixture]
    class DateTimeProviderTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void When_No_Context_Is_Given_It_Should_Return_Correc_tUtcNow()
        {
            var timeDifferenceTolerance = 1000;
            var result = DateTimeProvider.Instance.GetUtcNow();
            result.Should().BeCloseTo(DateTime.UtcNow, timeDifferenceTolerance);
        }
    }
}
