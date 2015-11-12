using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class Tests
  {
    [Test]
    public void Should_be_true()
    {
      Assert.That(true, Is.EqualTo(true));
    }
  }
}
