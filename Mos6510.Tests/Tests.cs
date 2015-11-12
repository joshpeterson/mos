using NUnit.Framework;
using Should;

namespace Mos6510.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Should_be_true()
        {
            true.ShouldBeTrue();
        }
    }
}
