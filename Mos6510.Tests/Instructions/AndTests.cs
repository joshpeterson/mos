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

    [TestCase(AddressingMode.AbsoluteX, RegisterName.X)]
    [TestCase(AddressingMode.AbsoluteY, RegisterName.Y)]
    public void AbsoluteIndexedModeAndsWithTheAccumulator(AddressingMode mode,
                                                          RegisterName register)
    {
      accumulator.SetValue(0xA);
      memory.SetValue(0x1010, 0x8);
      model.GetRegister(register).SetValue(0x10);
      and.Execute(model, memory, mode, 0x1000);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x8));
    }

    [Test]
    public void ZeropageModeAndsWithTheAccumulator()
    {
      accumulator.SetValue(0xA);
      memory.SetValue(0x0C0, 0x8);
      and.Execute(model, memory, AddressingMode.Zeropage, 0xC0);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x8));
    }

    [TestCase(AddressingMode.ZeropageX, RegisterName.X)]
    [TestCase(AddressingMode.ZeropageY, RegisterName.Y)]
    public void ZeropageIndexedModeAndsWithTheAccumulator(AddressingMode mode,
                                                          RegisterName register)
    {
      accumulator.SetValue(0xA);
      memory.SetValue(0x0070, 0x8);
      model.GetRegister(register).SetValue(0x10);
      and.Execute(model, memory, mode, 0x60);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x8));
    }

    [TestCase(AddressingMode.IndexedIndirectX, RegisterName.X)]
    [TestCase(AddressingMode.IndexedIndirectY, RegisterName.Y)]
    public void IndexedIndirectModeAndsWithTheAccumulator(AddressingMode mode,
                                                          RegisterName register)
    {
      accumulator.SetValue(0xA);
      memory.SetValue(0x8000, 0x8);
      memory.SetValue(0x0070, 0x00);
      memory.SetValue(0x0071, 0x80);
      model.GetRegister(register).SetValue(0x10);
      and.Execute(model, memory, mode, 0x60);
      Assert.That(accumulator.GetValue(), Is.EqualTo(0x8));
    }

    [TestCase(AddressingMode.Immediate, 2)]
    [TestCase(AddressingMode.Absolute, 4)]
    [TestCase(AddressingMode.AbsoluteX, 4)]
    [TestCase(AddressingMode.AbsoluteY, 4)]
    [TestCase(AddressingMode.Zeropage, 3)]
    [TestCase(AddressingMode.ZeropageX, 4)]
    [TestCase(AddressingMode.ZeropageY, 4)]
    [TestCase(AddressingMode.IndexedIndirectX, 6)]
    [TestCase(AddressingMode.IndexedIndirectY, 5)]
    public void ReturnsTheProperNumberOfCycles(AddressingMode mode, int expected)
    {
      Assert.That(and.CyclesFor(model, mode, 0), Is.EqualTo(expected));
    }

    [TestCase(AddressingMode.AbsoluteX, RegisterName.X)]
    [TestCase(AddressingMode.AbsoluteY, RegisterName.Y)]
    public void ReturnsTheProperNumberOfCyclesWhenCrossingPageBoundary(
        AddressingMode mode, RegisterName register)
    {
      memory.SetValue(0x10F0, 0);
      model.GetRegister(register).SetValue(0x10);
      Assert.That(and.CyclesFor(model, mode, 0x10F0), Is.EqualTo(5));
    }
  }
}
