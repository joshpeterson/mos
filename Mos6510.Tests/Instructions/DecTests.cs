using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class DecTests
  {
    const ushort address = 0x3400;
    ProgrammingModel model;
    Memory memory;
    Argument argument;

    [SetUp]
    public void SetUp()
    {
      model = new ProgrammingModel();
      memory = new Memory();
      argument = new Argument(0, address);
    }

    [TestCase(0x40, 0x3F)]
    [TestCase(0x00, 0xFF)]
    public void DecrementsAMemoryLocation(byte initialValue, byte finalValue)
    {
      memory.SetValue(address, initialValue);

      new Dec().Execute(model, memory, argument);
      Assert.That(memory.GetValue(address), Is.EqualTo(finalValue));
    }

    [TestCase(0x80, false)]
    [TestCase(0x81, true)]
    public void SetsTheNegativeFlag(byte initialValue, bool expectedNegativeFlag)
    {
      memory.SetValue(address, initialValue);
      model.NegativeFlag = !expectedNegativeFlag;

      new Dec().Execute(model, memory, argument);
      Assert.That(model.NegativeFlag, Is.EqualTo(expectedNegativeFlag));
    }

    [TestCase(0x00, false)]
    [TestCase(0x01, true)]
    public void SetsTheZeroFlag(byte initialValue, bool expectedZeroFlag)
    {
      memory.SetValue(address, initialValue);
      model.ZeroFlag = !expectedZeroFlag;

      new Dec().Execute(model, memory, argument);
      Assert.That(model.ZeroFlag, Is.EqualTo(expectedZeroFlag));
    }
  }
}
