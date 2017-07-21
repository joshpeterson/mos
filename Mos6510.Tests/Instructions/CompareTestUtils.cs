using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  public abstract class CompareTestUtils<T> where T : Instruction, new()
  {
    private RegisterName registerName;

    public CompareTestUtils(RegisterName registerName)
    {
      this.registerName = registerName;
    }

    [TestCase(0x90, true)]
    [TestCase(0x30, false)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(registerName).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      new T().Execute(model, null, new Argument(0x10, 0));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0xFF, false)]
    [TestCase(0x01, true)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(registerName).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new T().Execute(model, null, new Argument(0x01, 0));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0xFD, true)]
    [TestCase(0xFF, false)]
    public void VerifyValuesOfCarryFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(registerName).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new T().Execute(model, null, new Argument(0xFE,0));

      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }
  }
}
