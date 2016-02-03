using Mos6510;
using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class ProgrammingModelTests
  {
    [TestCase(RegisterName.A)]
    [TestCase(RegisterName.Y)]
    [TestCase(RegisterName.X)]
    [TestCase(RegisterName.PC)]
    [TestCase(RegisterName.S)]
    [TestCase(RegisterName.P)]
    public void CanSetTheValueInRegister(RegisterName name)
    {
      const int expectedValue = 42;

      var model = new ProgrammingModel();

      var registerBeforeSet = model.GetRegister(name);
      registerBeforeSet.SetValue(expectedValue);

      var registerAfterSet = model.GetRegister(name);
      Assert.That(registerAfterSet.GetValue(), Is.EqualTo(expectedValue));
    }

    [Test]
    public void SetsProperDefaultValueForStatusRegister()
    {
      var model = new ProgrammingModel();
      Assert.That(model.GetRegister(RegisterName.P).GetValue(), Is.EqualTo(0x20));
    }

    [TestCase(ProgrammingModel.NegativeFlagMask, true)]
    [TestCase(~ProgrammingModel.NegativeFlagMask, false)]
    public void VerifyNegativeFlagGetter(int value, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.P).SetValue(value);
      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, 0x80)]
    [TestCase(false, 0)]
    public void VerifyNegativeFlagSetter(bool value, int expectedResult)
    {
      var model = new ProgrammingModel();
      model.NegativeFlag = value;
      Assert.That(model.GetRegister(RegisterName.P).GetValue() &
          ProgrammingModel.NegativeFlagMask, Is.EqualTo(expectedResult));
    }
  }
}
