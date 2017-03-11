using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class EorTests
  {
    [Test]
    public void ExclusiveOrsWithTheAccumulator()
    {
      var model = new ProgrammingModel();
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xA);

      new Eor().Execute(model, null, new Argument(0xF, 0));
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x5));
    }

    [TestCase(0x80, false)]
    [TestCase(0x00, true)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      new Eor().Execute(model, null, new Argument(0x80,0));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x10, false)]
    [TestCase(0x01, true)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Eor().Execute(model, null, new Argument(0x01,0));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }
  }
}
