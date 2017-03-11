using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class OraTests
  {
    [Test]
    public void OrsWithTheAccumulator()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      new Ora().Execute(model, null, new Argument(0x1,0));
      Assert.That(accumulator.GetValue(), Is.EqualTo(0xB));
    }

    [TestCase(0x80, true)]
    [TestCase(0x00, false)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      new Ora().Execute(model, null, new Argument(0x7F,0));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Ora().Execute(model, null, new Argument(0x00,0));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }
  }
}
