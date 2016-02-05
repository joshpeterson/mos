using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class InyTests
  {
    [Test]
    public void IncrementsTheValueInRegisterY()
    {
      const int initialValue = 42;

      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.Y).SetValue(initialValue);
      var instruction = new Iny();
      instruction.Execute(model);
      Assert.That(model.GetRegister(RegisterName.Y).GetValue(),
                  Is.EqualTo(initialValue + 1));
    }

    [TestCase(0x7F, true)]
    [TestCase(0x7E, false)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.Y).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      var instruction = new Iny();
      instruction.Execute(model);

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0xFF, true)]
    [TestCase(0x00, false)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.Y).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      var instruction = new Iny();
      instruction.Execute(model);

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }
  }
}
