using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Instructions.Tests
{
  [TestFixture]
  public class AndTests
  {
    private ProgrammingModel model;
    private Memory memory;
    private Register accumulator;
    private And and;

    [SetUp]
    public void SetUp()
    {
      model = new ProgrammingModel();
      memory = new Memory();
      accumulator = model.GetRegister(RegisterName.A);
      and = new And();
    }

    [Test]
    public void ImmediateModeAndsWithTheAccumulator()
    {
      accumulator.SetValue(0xA);
      and.Execute(model, memory, AddressingMode.Immediate, 0x5);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0));
    }

    [Test]
    public void AbsoluteModeAndsWithTheAccumulator()
    {
      accumulator.SetValue(0xA);
      memory.SetValue(0x1000, 0x8);
      and.Execute(model, memory, AddressingMode.Absolute, 0x1000);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x8));
    }

    [TestCase(AddressingMode.Immediate, 0, 2)]
    [TestCase(AddressingMode.Absolute, 0, 4)]
    public void ReturnsTheProperNumberOfCycles(AddressingMode mode, int operand,
                                                int expected)
    {
      Assert.That(and.Execute(model, memory, mode, (ushort)operand),
                  Is.EqualTo(expected));
    }
  }
}
