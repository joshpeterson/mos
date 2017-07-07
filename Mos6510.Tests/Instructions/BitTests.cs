using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class BitTests
  {
    [TestCase(0x82, true)]
    [TestCase(0x7F, false)]
    public void NegativeFlagIsSetAccordingToTheHighBitOfMemoryInTheArgument(
      byte memoryValue, bool expectedNegativeFlag)
    {
      var model = new ProgrammingModel();
      model.NegativeFlag = !expectedNegativeFlag;

      const ushort address = 0xD000;
      var memory = new Memory();
      memory.SetValue(address, memoryValue);

      new Bit().Execute(model, memory, new Argument(0, address));
      Assert.That(model.NegativeFlag, Is.EqualTo(expectedNegativeFlag),
                  "The negative flag does not have the expected value.");
    }

    [TestCase(0x43, true)]
    [TestCase(0x3F, false)]
    public void OverflowFlagIsSetAccordingToBitSixOfMemoryInTheArgument(
      byte memoryValue, bool expectedOverflowFlag)
    {
      var model = new ProgrammingModel();
      model.OverflowFlag = !expectedOverflowFlag;

      const ushort address = 0xD000;
      var memory = new Memory();
      memory.SetValue(address, memoryValue);

      new Bit().Execute(model, memory, new Argument(0, address));
      Assert.That(model.OverflowFlag, Is.EqualTo(expectedOverflowFlag),
                  "The overflow flag does not have the expected value.");
    }

    [TestCase(0x3F, 0x40, true)]
    [TestCase(0x3F, 0x03, false)]
    public void ZeroFlagIsSetiByAndingTheAccumulatorWithMemoryInTheArgument(
      int accumulatorValue, byte memoryValue, bool expectedZeroFlag)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(accumulatorValue);
      model.ZeroFlag = !expectedZeroFlag;

      const ushort address = 0xD000;
      var memory = new Memory();
      memory.SetValue(address, memoryValue);

      new Bit().Execute(model, memory, new Argument(0, address));
      Assert.That(model.ZeroFlag, Is.EqualTo(expectedZeroFlag),
                  "The zero flag does not have the expected value.");
    }
  }
}
