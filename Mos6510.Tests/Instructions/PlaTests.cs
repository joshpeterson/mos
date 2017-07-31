using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class PlaTests
  {
    [Test]
    public void StoresTheFromTheTopOfTheStackValueInTheAccumulator()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      const byte expectedValue = 0x79;
      Stack.Push(model, memory, expectedValue);

      new Pla().Execute(model, memory, new Argument(0, 0));
      Assert.That(model.GetRegister(RegisterName.A).GetValue(),
                  Is.EqualTo(expectedValue));
    }

    [Test]
    public void PopsTheStack()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      const byte previousStackValue = 0x42;
      Stack.Push(model, memory, 0x42);
      Stack.Push(model, memory, 0x79);

      new Pla().Execute(model, memory, new Argument(0, 0));
      Assert.That(Stack.Top(model, memory), Is.EqualTo(previousStackValue));
    }
  }
}
