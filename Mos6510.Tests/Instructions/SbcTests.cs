using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class SbcTests
  {
    [Test]
    public void SubtractsFromTheAccumulator()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      new Sbc().Execute(model, null, new Argument(0x8, 0));
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x2));
    }

    [Test]
    public void SubstractsFromTheAccumulatorIncludingBorrow()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      var status = model.GetRegister(RegisterName.S);
      status.SetValue(status.GetValue() | 0x01);

      new Sbc().Execute(model, null, new Argument(0x8, 0));
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x1));
    }

    [TestCase(0x90, true)]
    [TestCase(0x30, false)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      new Sbc().Execute(model, null, new Argument(0x10, 0));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0xFF, false)]
    [TestCase(0x01, true)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Sbc().Execute(model, null, new Argument(0x01, 0));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfOverflowFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Sbc().Execute(model, null, new Argument(0x01,0));

      Assert.That(model.OverflowFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0xFD, true)]
    [TestCase(0xFF, false)]
    public void VerifyValuesOfCarryFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Sbc().Execute(model, null, new Argument(0xFE,0));

      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }
  }
}
