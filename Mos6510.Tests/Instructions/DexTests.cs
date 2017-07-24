using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class DexTests
  {
    ProgrammingModel model;
    Memory memory;
    Argument argument;
    Register x;

    [SetUp]
    public void SetUp()
    {
      model = new ProgrammingModel();
      memory = new Memory();
      argument = new Argument(0, 0);
      x = model.GetRegister(RegisterName.X);
    }

    [TestCase(0x40, 0x3F)]
    [TestCase(0x00, 0xFF)]
    public void DecrementsTheXRegister(byte initialValue, byte finalValue)
    {
      x.SetValue(initialValue);

      new Dex().Execute(model, memory, argument);
      Assert.That(x.GetValue(), Is.EqualTo(finalValue));
    }

    [TestCase(0x80, false)]
    [TestCase(0x81, true)]
    public void SetsTheNegativeFlag(byte initialValue, bool expectedNegativeFlag)
    {
      x.SetValue(initialValue);
      model.NegativeFlag = !expectedNegativeFlag;

      new Dex().Execute(model, memory, argument);
      Assert.That(model.NegativeFlag, Is.EqualTo(expectedNegativeFlag));
    }

    [TestCase(0x00, false)]
    [TestCase(0x01, true)]
    public void SetsTheZeroFlag(byte initialValue, bool expectedZeroFlag)
    {
      x.SetValue(initialValue);
      model.ZeroFlag = !expectedZeroFlag;

      new Dex().Execute(model, memory, argument);
      Assert.That(model.ZeroFlag, Is.EqualTo(expectedZeroFlag));
    }
  }
}
