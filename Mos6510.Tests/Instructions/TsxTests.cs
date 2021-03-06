using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class TsxTests
  {
    [Test]
    public void CopiesTheStackPointerToTheXRegister()
    {
      var model = new ProgrammingModel();
      const byte expectedValue = 0x92;
      model.GetRegister(RegisterName.S).SetValue(expectedValue);

      new Tsx().Execute(model, null, null);

      Assert.That(model.GetRegister(RegisterName.X).GetValue(),
                  Is.EqualTo(expectedValue));
    }

    [TestCase(0x80, true)]
    [TestCase(0x7F, false)]
    public void SetsTheNegativeFlagIfTheHighBitIsOne(byte value,
        bool expectedNegativeFlag)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.S).SetValue(value);
      model.NegativeFlag = !expectedNegativeFlag;

      new Tsx().Execute(model, null, null);

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedNegativeFlag));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void SetsTheZeroFlagIfTheValueTransferredIsZero(byte value,
        bool expectedZeroFlag)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.S).SetValue(value);
      model.ZeroFlag = !expectedZeroFlag;

      new Tsx().Execute(model, null, null);

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedZeroFlag));
    }
  }
}
