using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class StaTests
  {
    [Test]
    public void PutsItsTheAccumulatorInTheAddress()
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

    [TestCase(AddressingMode.Absolute, 4)]
    [TestCase(AddressingMode.AbsoluteX, 5)]
    [TestCase(AddressingMode.AbsoluteY, 5)]
    [TestCase(AddressingMode.Zeropage, 3)]
    [TestCase(AddressingMode.ZeropageX, 4)]
    [TestCase(AddressingMode.ZeropageY, 4)]
    [TestCase(AddressingMode.IndexedIndirectX, 6)]
    [TestCase(AddressingMode.IndexedIndirectY, 6)]
    public void ReturnsTheProperNumberOfCycles(AddressingMode mode, int expected)
    {
      Assert.That(new Sta().CyclesFor(mode), Is.EqualTo(expected));
    }
  }
}
