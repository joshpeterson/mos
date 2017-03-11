using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class LdyTests
  {
    [Test]
    public void PutsItsArgumentInTheAccumulator()
    {
      var model = new ProgrammingModel();
      var y = model.GetRegister(RegisterName.Y);
      y.SetValue(0xA);

      new Ldy().Execute(model, null, new Argument(0x8, 0));
      Assert.That(y.GetValue(), Is.EqualTo(0x8));
    }

    [TestCase(0x80, true)]
    [TestCase(0x30, false)]
    public void VerifyValuesOfNegativeFlag(byte newValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.NegativeFlag = !expectedResult;

      new Ldy().Execute(model, null, new Argument(newValue, 0));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlag(byte newValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.ZeroFlag = !expectedResult;

      new Ldy().Execute(model, null, new Argument(newValue,0));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }
  }
}
