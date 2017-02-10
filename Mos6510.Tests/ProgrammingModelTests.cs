using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class ProgrammingModelTests
  {
    private ProgrammingModel model;

    [SetUp]
    public void SetUp()
    {
      model = new ProgrammingModel();
    }

    [TestCase(RegisterName.A)]
    [TestCase(RegisterName.Y)]
    [TestCase(RegisterName.X)]
    [TestCase(RegisterName.PC)]
    [TestCase(RegisterName.S)]
    [TestCase(RegisterName.P)]
    public void CanSetTheValueInRegister(RegisterName name)
    {
      const int expectedValue = 42;

      var registerBeforeSet = model.GetRegister(name);
      registerBeforeSet.SetValue(expectedValue);

      var registerAfterSet = model.GetRegister(name);
      Assert.That(registerAfterSet.GetValue(), Is.EqualTo(expectedValue));
    }

    [Test]
    public void SetsProperDefaultValueForStatusRegister()
    {
      Assert.That(model.GetRegister(RegisterName.P).GetValue(), Is.EqualTo(0x20));
    }

    [TestCase(0x01, true)]
    [TestCase(~0x01, false)]
    public void VerifyCarryFlagGetter(int value, bool expectedResult)
    {
      model.GetRegister(RegisterName.P).SetValue(value);
      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, 0x01)]
    [TestCase(false, 0)]
    public void VerifyCarryFlagSetter(bool value, int expectedResult)
    {
      model.CarryFlag = value;
      Assert.That(model.GetRegister(RegisterName.P).GetValue() &
                  0x01, Is.EqualTo(expectedResult));
    }

    [TestCase(0x02, true)]
    [TestCase(~0x02, false)]
    public void VerifyZeroFlagGetter(int value, bool expectedResult)
    {
      model.GetRegister(RegisterName.P).SetValue(value);
      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, 0x02)]
    [TestCase(false, 0)]
    public void VerifyZeroFlagSetter(bool value, int expectedResult)
    {
      model.ZeroFlag = value;
      Assert.That(model.GetRegister(RegisterName.P).GetValue() &
                  0x03, Is.EqualTo(expectedResult));
    }

    [TestCase(0x04, true)]
    [TestCase(~0x04, false)]
    public void VerifyIrqDisableFlagGetter(int value, bool expectedResult)
    {
      model.GetRegister(RegisterName.P).SetValue(value);
      Assert.That(model.IrqDisableFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, 0x04)]
    [TestCase(false, 0)]
    public void VerifyIrqDisableFlagSetter(bool value, int expectedResult)
    {
      model.IrqDisableFlag = value;
      Assert.That(model.GetRegister(RegisterName.P).GetValue() &
                  0x04, Is.EqualTo(expectedResult));
    }

    [TestCase(0x08, true)]
    [TestCase(~0x08, false)]
    public void VerifyDecimalModeFlagGetter(int value, bool expectedResult)
    {
      model.GetRegister(RegisterName.P).SetValue(value);
      Assert.That(model.DecimalModeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, 0x08)]
    [TestCase(false, 0)]
    public void VerifyDecimalModeFlagSetter(bool value, int expectedResult)
    {
      model.DecimalModeFlag = value;
      Assert.That(model.GetRegister(RegisterName.P).GetValue() &
                  0x08, Is.EqualTo(expectedResult));
    }

    [TestCase(0x10, true)]
    [TestCase(~0x10, false)]
    public void VerifyBreakCommandFlagGetter(int value, bool expectedResult)
    {
      model.GetRegister(RegisterName.P).SetValue(value);
      Assert.That(model.BreakCommandFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, 0x10)]
    [TestCase(false, 0)]
    public void VerifyBreakCommandFlagSetter(bool value, int expectedResult)
    {
      model.BreakCommandFlag = value;
      Assert.That(model.GetRegister(RegisterName.P).GetValue() &
                  0x10, Is.EqualTo(expectedResult));
    }

    [TestCase(0x40, true)]
    [TestCase(~0x40, false)]
    public void VerifyOverflowFlagGetter(int value, bool expectedResult)
    {
      model.GetRegister(RegisterName.P).SetValue(value);
      Assert.That(model.OverflowFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, 0x40)]
    [TestCase(false, 0)]
    public void VerifyOverflowFlagSetter(bool value, int expectedResult)
    {
      model.OverflowFlag = value;
      Assert.That(model.GetRegister(RegisterName.P).GetValue() &
                  0x40, Is.EqualTo(expectedResult));
    }

    [TestCase(0x80, true)]
    [TestCase(~0x80, false)]
    public void VerifyNegativeFlagGetter(int value, bool expectedResult)
    {
      model.GetRegister(RegisterName.P).SetValue(value);
      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, 0x80)]
    [TestCase(false, 0)]
    public void VerifyNegativeFlagSetter(bool value, int expectedResult)
    {
      model.NegativeFlag = value;
      Assert.That(model.GetRegister(RegisterName.P).GetValue() &
                  0x80, Is.EqualTo(expectedResult));
    }

  }
}
