using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class StxTests
  {
    [Test]
    public void StoresTheXRegisterInMemory()
    {
      var model = new ProgrammingModel();
      var x = model.GetRegister(RegisterName.X);
      var memory = new Memory();
      const byte expectedValue = 0xA;
      x.SetValue(expectedValue);

      const ushort address = 0x1008;
      new Stx().Execute(model, memory, new Argument(0, address));
      Assert.That(memory.GetValue(address), Is.EqualTo(expectedValue));
    }
  }
}
