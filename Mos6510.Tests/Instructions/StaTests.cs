using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class StaTests
  {
    [Test]
    public void StoresTheAccumulatorInMemory()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      var memory = new Memory();
      const byte expectedValue = 0xA;
      accumulator.SetValue(expectedValue);

      const ushort address = 0x1008;
      new Sta().Execute(model, memory, new Argument(0,address));
      Assert.That(memory.GetValue(address), Is.EqualTo(expectedValue));
    }
  }
}
