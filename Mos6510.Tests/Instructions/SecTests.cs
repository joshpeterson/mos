using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class SecTests
  {
    [Test]
    public void SetsTheCarryFlag()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      model.CarryFlag = false;

      new Sec().Execute(model, memory, new Argument(0, 0));

      Assert.That(model.CarryFlag, Is.True,
                  "The carry flag is not set, which is not expected.");
    }
  }
}
