using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class CldTests
  {
    [Test]
    public void ClearsTheDecimalModeFlag()
    {
      var model = new ProgrammingModel();
      model.DecimalModeFlag = true;

      new Cld().Execute(model, null, new Argument(0,0));

      Assert.That(model.DecimalModeFlag, Is.False,
                  "The irq decimcal flag was not cleared, which is not expected.");
    }
  }
}
