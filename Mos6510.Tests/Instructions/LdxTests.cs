using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class LdxTests
  {
    [Test]
    public void PutsItsArgumentInTheAccumulator()
    {
      var model = new ProgrammingModel();
      var x = model.GetRegister(RegisterName.X);
      x.SetValue(0xA);

      new Ldx().Execute(model, null, new Argument(0x8, 0));
      Assert.That(x.GetValue(), Is.EqualTo(0x8));
    }

    [TestCase(0x80, true)]
    [TestCase(0x30, false)]
    public void VerifyValuesOfNegativeFlag(byte newValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.NegativeFlag = !expectedResult;

      new Ldx().Execute(model, null, new Argument(newValue, 0));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlag(byte newValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.ZeroFlag = !expectedResult;

      new Ldx().Execute(model, null, new Argument(newValue,0));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(AddressingMode.Immediate, 2)]
    [TestCase(AddressingMode.Absolute, 4)]
    [TestCase(AddressingMode.AbsoluteY, 4)]
    [TestCase(AddressingMode.Zeropage, 3)]
    [TestCase(AddressingMode.ZeropageY, 4)]
    public void ReturnsTheProperNumberOfCycles(AddressingMode mode, int expected)
    {
      Assert.That(new Ldx().CyclesFor(mode), Is.EqualTo(expected));
    }
  }
}
