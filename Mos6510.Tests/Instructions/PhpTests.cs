using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class PhpTests
  {
    [Test]
    public void PushesTheStatusRegisterOnTheTopOfTheStack()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      const byte expectedValue = 0x79;
      model.GetRegister(RegisterName.P).SetValue(expectedValue);

      new Php().Execute(model, memory, new Argument(0, 0));
      Assert.That(Stack.Top(model, memory), Is.EqualTo(expectedValue));
    }
  }
}
