using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  class AdcTests
  {
    [Test]
    public void AddsWithTheAccumulator()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      new Adc().Execute(model, null, new Argument(0x8,0));
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x12));
    }

    [Test]
    public void AddsWithTheAccumulatorIncludingCarry()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      var status = model.GetRegister(RegisterName.P);
      status.SetValue(status.GetValue() | 0x01);

      new Adc().Execute(model, null, new Argument(0x8,0));
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x13));
    }

    [TestCase(0x70, true)]
    [TestCase(0x30, false)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      new Adc().Execute(model, null, new Argument(0x10,0));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0xFF, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Adc().Execute(model, null, new Argument(0x01,0));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x7F, true)]
    [TestCase(0x7E, false)]
    public void VerifyValuesOfOverflowFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Adc().Execute(model, null, new Argument(0x01,0));

      Assert.That(model.OverflowFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0xFF, true)]
    [TestCase(0x00, false)]
    public void VerifyValuesOfCarryFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Adc().Execute(model, null, new Argument(0xFF,0));

      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }
  }
}
