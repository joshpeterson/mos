using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class StyTests
  {
    [Test]
    public void StoresTheYRegisterInMemory()
    {
      var model = new ProgrammingModel();
      var y = model.GetRegister(RegisterName.Y);
      var memory = new Memory();
      const byte expectedValue = 0xA;
      y.SetValue(expectedValue);

      const ushort address = 0x1008;
      new Sty().Execute(model, memory, new Argument(0,address));
      Assert.That(memory.GetValue(address), Is.EqualTo(expectedValue));
    }
  }
}
