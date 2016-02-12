using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Instructions.Tests
{
  [TestFixture]
  public class AndTests
  {
    private ProgrammingModel model;
    private Register accumulator;
    private And and;

    [SetUp]
    public void SetUp()
    {
      model = new ProgrammingModel();
      accumulator = model.GetRegister(RegisterName.A);
      and = new And();
    }

    [TestCase(AddressingMode.Immediate, 0xA, 0x5, 0)]
    public void AndsWithTheAccumulator(AddressingMode mode, int initial,
                                        int operand, int expected)
    {
      accumulator.SetValue((byte)initial);
      and.Execute(model, AddressingMode.Immediate, (ushort)operand);
      Assert.That(accumulator.GetValue(), Is.EqualTo(expected));
    }

    [TestCase(AddressingMode.Immediate, 0, 2)]
    public void ReturnsTheProperNumberOfCycles(AddressingMode mode, int operand,
                                                int expected)
    {
      Assert.That(and.Execute(model, mode, (ushort)operand), Is.EqualTo(expected));
    }
  }
}
