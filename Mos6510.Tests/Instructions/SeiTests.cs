using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class SeiTests
  {
    [Test]
    public void SetsTheIrqDisableFlag()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      model.IrqDisableFlag = false;

      new Sei().Execute(model, memory, new Argument(0, 0));

      Assert.That(model.IrqDisableFlag, Is.True,
                  "The interrupt disable flag is not set, which is not expected.");
    }
  }
}
