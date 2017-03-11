using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class ClcTests
  {
    [Test]
    public void ClearsTheCarryFlag()
    {
      var model = new ProgrammingModel();
      model.CarryFlag = true;

      new Clc().Execute(model, null, new Argument(0,0));

      Assert.That(model.CarryFlag, Is.False,
                  "The carry flag was not cleared, which is not expected.");
    }
  }
}
