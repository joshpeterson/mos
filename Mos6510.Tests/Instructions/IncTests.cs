using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class IncTests
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

    [TestCase(0x40, 0x41)]
    [TestCase(0xFF, 0x00)]
    public void IncrementsAMemoryLocation(byte initialValue, byte finalValue)
    {
      memory.SetValue(address, initialValue);

      new Inc().Execute(model, memory, argument);
      Assert.That(memory.GetValue(address), Is.EqualTo(finalValue));
    }

    [TestCase(0xFF, false)]
    [TestCase(0x7F, true)]
    public void SetsTheNegativeFlag(byte initialValue, bool expectedNegativeFlag)
    {
      memory.SetValue(address, initialValue);
      model.NegativeFlag = !expectedNegativeFlag;

      new Inc().Execute(model, memory, argument);
      Assert.That(model.NegativeFlag, Is.EqualTo(expectedNegativeFlag));
    }

    [TestCase(0x00, false)]
    [TestCase(0xFF, true)]
    public void SetsTheZeroFlag(byte initialValue, bool expectedZeroFlag)
    {
      memory.SetValue(address, initialValue);
      model.ZeroFlag = !expectedZeroFlag;

      new Inc().Execute(model, memory, argument);
      Assert.That(model.ZeroFlag, Is.EqualTo(expectedZeroFlag));
    }
  }
}
