using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class RolTests
  {
    [TestCase(false, 0x54)]
    [TestCase(true, 0x55)]
    public void ShiftsTheBitsInTheAccumulator(bool carryFlag, int expectedResult)
    {
      var model = new ProgrammingModel();

      model.CarryFlag = carryFlag;
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xAA);

      new Rol().Execute(model, null, new AccumulatorArgument());
      Assert.That(accumulator.GetValue(), Is.EqualTo(expectedResult));
    }

    [TestCase(false, 0x7E)]
    [TestCase(true, 0x7F)]
    public void ShiftsTheBitsInTheMemoryLocation(bool carryFlag,
        int expectedResult)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      model.CarryFlag = carryFlag;
      const ushort address = 0x2400;
      const byte initialValue = 0xBF;
      memory.SetValue(address, initialValue);

      new Rol().Execute(model, memory, new Argument(initialValue, address));
      Assert.That(memory.GetValue(address), Is.EqualTo(expectedResult));
    }

    [TestCase(0x80, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlagWithAccumulator(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.ZeroFlag = !expectedResult;

      new Rol().Execute(model, null, new AccumulatorArgument());

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x80, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlagWithMemoryLocation(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      const ushort address = 0x1600;
      memory.SetValue(address, (byte)initialValue);
      model.ZeroFlag = !expectedResult;

      new Rol().Execute(model, memory, new Argument((byte)initialValue, address));

      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x40, true)]
    [TestCase(0x80, false)]
    public void VerifyValuesOfNegativeFlagWithAccumulator(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.NegativeFlag = !expectedResult;

      new Rol().Execute(model, null, new AccumulatorArgument());

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x40, true)]
    [TestCase(0x80, false)]
    public void VerifyValuesOfNegativeFlagWithMemoryLocation(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      const ushort address = 0x1600;
      memory.SetValue(address, (byte)initialValue);
      model.NegativeFlag = !expectedResult;

      new Rol().Execute(model, memory, new Argument((byte)initialValue, address));

      Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x80, true)]
    [TestCase(0x7F, false)]
    public void VerifyValuesOfCarryFlagWithAccumulator(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.CarryFlag = !expectedResult;

      new Rol().Execute(model, null, new AccumulatorArgument());

      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x80, true)]
    [TestCase(0x7F, false)]
    public void VerifyValuesOfCarryFlagWithMemoryLocation(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      const ushort address = 0x1600;
      memory.SetValue(address, (byte)initialValue);
      model.CarryFlag = !expectedResult;

      new Rol().Execute(model, memory, new Argument((byte)initialValue, address));

      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }
  }
}
