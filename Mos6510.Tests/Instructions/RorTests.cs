using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class RorTests
  {
    [TestCase(false, 0x55)]
    [TestCase(true, 0xD5)]
    public void ShiftsTheBitsInTheAccumulator(bool carryFlag, int expectedResult)
    {
      var model = new ProgrammingModel();

      model.CarryFlag = carryFlag;
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xAA);

      new Ror().Execute(model, null, new AccumulatorArgument());
      Assert.That(accumulator.GetValue(), Is.EqualTo(expectedResult));
    }

    [TestCase(false, 0x5F)]
    [TestCase(true, 0xDF)]
    public void ShiftsTheBitsInTheMemoryLocation(bool carryFlag,
        int expectedResult)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      model.CarryFlag = carryFlag;
      const ushort address = 0x2400;
      const byte initialValue = 0xBF;
      memory.SetValue(address, initialValue);

      new Ror().Execute(model, memory, new Argument(initialValue, address));
      Assert.That(memory.GetValue(address), Is.EqualTo(expectedResult));
    }

    [TestCase(0x01, true)]
    [TestCase(0xD0, false)]
    public void VerifyValuesOfZeroFlagWithAccumulator(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Ror().Execute(model, null, new AccumulatorArgument());

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x01, true)]
    [TestCase(0xD0, false)]
    public void VerifyValuesOfZeroFlagWithMemoryLocation(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      const ushort address = 0x1600;
      memory.SetValue(address, (byte)initialValue);
      model.ZeroFlag = !expectedResult;

      new Ror().Execute(model, memory, new Argument((byte)initialValue, address));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, true)]
    [TestCase(false, false)]
    public void VerifyValuesOfNegativeFlagWithAccumulator(bool carryFlag,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.CarryFlag = carryFlag;
      model.GetRegister(RegisterName.A).SetValue(0XC8);
      model.NegativeFlag = !expectedResult;

      new Ror().Execute(model, null, new AccumulatorArgument());

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(true, true)]
    [TestCase(false, false)]
    public void VerifyValuesOfNegativeFlagWithMemoryLocation(bool carryFlag,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.CarryFlag = carryFlag;
      var memory = new Memory();
      const ushort address = 0x1600;
      memory.SetValue(address, 0x36);
      model.NegativeFlag = !expectedResult;

      new Ror().Execute(model, memory, new Argument(0x36, address));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x01, true)]
    [TestCase(0xFE, false)]
    public void VerifyValuesOfCarryFlagWithAccumulator(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.CarryFlag = !expectedResult;

      new Ror().Execute(model, null, new AccumulatorArgument());

      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x01, true)]
    [TestCase(0xFE, false)]
    public void VerifyValuesOfCarryFlagWithMemoryLocation(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      const ushort address = 0x1600;
      memory.SetValue(address, (byte)initialValue);
      model.CarryFlag = !expectedResult;

      new Ror().Execute(model, memory, new Argument((byte)initialValue, address));

      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }
  }
}
