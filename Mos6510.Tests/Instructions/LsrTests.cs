using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class LsrTests
  {
    public void ShiftsTheBitsInTheAccumulator()
    {
      var model = new ProgrammingModel();

      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xAA);

      new Lsr().Execute(model, null, new AccumulatorArgument());
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x55));
    }

    public void ShiftsTheBitsInTheMemoryLocation()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      const ushort address = 0x2400;
      const byte initialValue = 0xBF;
      memory.SetValue(address, initialValue);

      new Lsr().Execute(model, memory, new Argument(initialValue, address));
      Assert.That(memory.GetValue(address), Is.EqualTo(0x5F));
    }

    [Test]
    public void ClearsNegativeFlag()
    {
      var model = new ProgrammingModel();

      model.NegativeFlag = true;
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue(0xAA);

      new Lsr().Execute(model, null, new AccumulatorArgument());
      Assert.That(model.NegativeFlag, Is.False,
                  "The negative flag was not cleared, which is not expected.");
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlagWithAccumulator(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();

      model.ZeroFlag = !expectedResult;
      model.GetRegister(RegisterName.A).SetValue(initialValue);

      new Lsr().Execute(model, null, new AccumulatorArgument());
      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlagWithMemoryLocation(byte initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      model.ZeroFlag = !expectedResult;
      const ushort address = 0x2400;
      memory.SetValue(address, initialValue);

      new Lsr().Execute(model, memory, new Argument(initialValue, address));
      Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x01, true)]
    [TestCase(0xFE, false)]
    public void VerifyValuesOfCarryFlagWithAccumulator(int initialValue,
        bool expectedResult)
    {
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.A).SetValue(initialValue);
      model.CarryFlag = !expectedResult;

      new Lsr().Execute(model, null, new AccumulatorArgument());
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

      new Lsr().Execute(model, memory, new Argument((byte)initialValue, address));
      Assert.That(model.CarryFlag, Is.EqualTo(expectedResult));
    }
  }
}
