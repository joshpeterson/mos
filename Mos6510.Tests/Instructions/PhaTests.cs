using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class PhaTests
  {
    [Test]
    public void PushesTheAccumulatorOnTheTopOfTheStack()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      const byte expectedValue = 0x79;
      model.GetRegister(RegisterName.A).SetValue(expectedValue);

      new Pha().Execute(model, memory, new Argument(0, 0));
      Assert.That(Stack.Top(model, memory), Is.EqualTo(expectedValue));
    }
  }
}
