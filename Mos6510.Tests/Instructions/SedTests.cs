using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class SedTests
  {
    [Test]
    public void SetsTheDecimalModeFlag()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      model.DecimalModeFlag = false;

      new Sed().Execute(model, memory, new Argument(0, 0));

      Assert.That(model.DecimalModeFlag, Is.True,
                  "The decimal mode flag is not set, which is not expected.");
    }
  }
}
