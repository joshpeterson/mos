using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class LdaTests
  {
    [Test]
    public void PutsItsArgumentInTheAccumulator()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      new Lda().Execute(model, null, 0x8);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x8));
    }

    [TestCase(0x80, true)]
    [TestCase(0x30, false)]
    public void VerifyValuesOfNegativeFlag(byte newValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.NegativeFlag = !expectedResult;

      new Lda().Execute(model, null, newValue);

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlag(byte newValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.ZeroFlag = !expectedResult;

      new Lda().Execute(model, null, newValue);

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(AddressingMode.Immediate, 2)]
    [TestCase(AddressingMode.Absolute, 4)]
    [TestCase(AddressingMode.AbsoluteX, 4)]
    [TestCase(AddressingMode.AbsoluteY, 4)]
    [TestCase(AddressingMode.Zeropage, 3)]
    [TestCase(AddressingMode.ZeropageX, 4)]
    [TestCase(AddressingMode.IndexedIndirectX, 6)]
    [TestCase(AddressingMode.IndexedIndirectY, 5)]
    public void ReturnsTheProperNumberOfCycles(AddressingMode mode, int expected)
    {
      Assert.That(new Lda().CyclesFor(mode), Is.EqualTo(expected));
    }
  }
}
