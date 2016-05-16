using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class EorTests
  {
    [Test]
    public void ExclusiveOrsWithTheAccumulator()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      new Eor().Execute(model, null, 0xF);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x5));
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
      Assert.That(new Eor().CyclesFor(mode), Is.EqualTo(expected));
    }

    [TestCase(0x80, false)]
    [TestCase(0x00, true)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      new Eor().Execute(model, null, 0x80);

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x10, false)]
    [TestCase(0x01, true)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Eor().Execute(model, null, 0x01);

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }
  }
}
