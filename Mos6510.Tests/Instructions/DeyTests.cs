using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class DeyTests
  {
    ProgrammingModel model;
    Memory memory;
    Argument argument;
    Register y;

    [SetUp]
    public void SetUp()
    {
      model = new ProgrammingModel();
      memory = new Memory();
      argument = new Argument(0, 0);
      y = model.GetRegister(RegisterName.Y);
    }

    [TestCase(0x40, 0x3F)]
    [TestCase(0x00, 0xFF)]
    public void DecrementsTheYRegister(byte initialValue, byte finalValue)
    {
      y.SetValue(initialValue);

      new Dey().Execute(model, memory, argument);
      Assert.That(y.GetValue(), Is.EqualTo(finalValue));
    }

    [TestCase(0x80, false)]
    [TestCase(0x81, true)]
    public void SetsTheNegativeFlag(byte initialValue, bool expectedNegativeFlag)
    {
      y.SetValue(initialValue);
      model.NegativeFlag = !expectedNegativeFlag;

      new Dey().Execute(model, memory, argument);
      Assert.That(model.NegativeFlag, Is.EqualTo(expectedNegativeFlag));
    }

    [TestCase(0x00, false)]
    [TestCase(0x01, true)]
    public void SetsTheZeroFlag(byte initialValue, bool expectedZeroFlag)
    {
      y.SetValue(initialValue);
      model.ZeroFlag = !expectedZeroFlag;

      new Dey().Execute(model, memory, argument);
      Assert.That(model.ZeroFlag, Is.EqualTo(expectedZeroFlag));
    }
  }
}
