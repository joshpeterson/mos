using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class TyaTests
  {
    [Test]
    public void CopiesTheYRegisterToTheAccumulator()
    {
      var model = new ProgrammingModel();
      const byte expectedValue = 0x92;
      model.GetRegister(RegisterName.Y).SetValue(expectedValue);

      new Tya().Execute(model, null, null);

      Assert.That(model.GetRegister(RegisterName.A).GetValue(),
                  Is.EqualTo(expectedValue));
    }

    [TestCase(0x80, true)]
    [TestCase(0x7F, false)]
    public void SetsTheNegativeFlagIfTheHighBitIsOne(byte value,
        bool expectedNegativeFlag)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.Y).SetValue(value);
      model.NegativeFlag = !expectedNegativeFlag;

      new Tya().Execute(model, null, null);

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedNegativeFlag));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void SetsTheZeroFlagIfTheValueTransferredIsZero(byte value,
        bool expectedZeroFlag)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.Y).SetValue(value);
      model.ZeroFlag = !expectedZeroFlag;

      new Tya().Execute(model, null, null);

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedZeroFlag));
    }
  }
}
