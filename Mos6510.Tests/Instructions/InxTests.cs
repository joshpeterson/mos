using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class InxTests
  {
    [Test]
    public void IncrementsTheValueInRegisterX()
    {
      const int initialValue = 42;

      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.X).SetValue(initialValue);
      var instruction = new Inx();
      instruction.Execute(model, null, new Argument(0,0));
      Assert.That(model.GetRegister(RegisterName.X).GetValue(),
                  Is.EqualTo(initialValue + 1));
    }

    [TestCase(0x7F, true)]
    [TestCase(0x7E, false)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.X).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      var instruction = new Inx();
      instruction.Execute(model, null, new Argument(0,0));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0xFF, true)]
    [TestCase(0x00, false)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.X).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      var instruction = new Inx();
      instruction.Execute(model, null, new Argument(0,0));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }
  }
}
