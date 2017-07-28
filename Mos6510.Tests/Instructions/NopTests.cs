using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class NopTests
  {
    [Test]
    public void ReturnsSuccess()
    {
      Assert.That(new Nop().Execute(null, null, null), Is.EqualTo(Result.Success));
    }
  }
}
