using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class ClvTests
  {
    [Test]
    public void ClearsTheOverflowFlag()
    {
      var model = new ProgrammingModel();
      model.OverflowFlag = true;

      new Clv().Execute(model, null, new Argument(0,0));

      Assert.That(model.OverflowFlag, Is.False,
                  "The overflow flag was not cleared, which is not expected.");
    }
  }
}
