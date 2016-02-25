using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Instructions.Tests
{
  [TestFixture]
  public class AndTests
  {
    [Test]
    public void AndsWithTheAccumulator()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      new And().Execute(model, null, 0x8);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x8));
    }

    [TestCase(AddressingMode.Immediate, 2)]
    [TestCase(AddressingMode.Absolute, 4)]
    [TestCase(AddressingMode.AbsoluteX, 4)]
    [TestCase(AddressingMode.AbsoluteY, 4)]
    [TestCase(AddressingMode.Zeropage, 3)]
    [TestCase(AddressingMode.ZeropageX, 4)]
    [TestCase(AddressingMode.ZeropageY, 4)]
    [TestCase(AddressingMode.IndexedIndirectX, 6)]
    [TestCase(AddressingMode.IndexedIndirectY, 5)]
    public void ReturnsTheProperNumberOfCycles(AddressingMode mode, int expected)
    {
      Assert.That(new And().CyclesFor(mode), Is.EqualTo(expected));
    }
  }
}
