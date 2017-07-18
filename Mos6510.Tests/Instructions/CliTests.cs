using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class CliTests
  {
    [Test]
    public void ClearsTheIrqDisableFlag()
    {
      var model = new ProgrammingModel();
      model.IrqDisableFlag = true;

      new Cli().Execute(model, null, new Argument(0,0));

      Assert.That(model.IrqDisableFlag, Is.False,
                  "The irq disable flag was not cleared, which is not expected.");
    }
  }
}
