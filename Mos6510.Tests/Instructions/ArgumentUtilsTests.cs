using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class ArgumentUtilsTests
  {
    private ProgrammingModel model;
    private Memory memory;

    [SetUp]
    public void SetUp()
    {
      model = new ProgrammingModel();
      memory = new Memory();
    }

    [Test]
    public void ReturnsTheOperandForImmediateMode()
    {
      byte expectedArgument = 42;
      Assert.That(ArgumentUtils.ArgumentFor(model, memory,
                                            AddressingMode.Immediate,
                                            expectedArgument).value,
                  Is.EqualTo(expectedArgument));
    }

    [Test]
    public void ReturnsAZeroAddressForImmediateMode()
    {
      Assert.That(ArgumentUtils.ArgumentFor(model, memory,
                                            AddressingMode.Immediate,
                                            42).address,
                  Is.EqualTo(0));
    }

    [Test]
    public void UsesTheOperandAsTheEffectiveAddressForAbsoluteMode()
    {
      const byte expectedArgument = 0x08;
      const ushort address = 0x1000;
      memory.SetValue(address, expectedArgument);
      Assert.That(ArgumentUtils.ArgumentFor(model, memory,
                                            AddressingMode.Absolute,
                                            address).value,
                  Is.EqualTo(expectedArgument));
    }

    [TestCase(AddressingMode.AbsoluteX, RegisterName.X)]
    [TestCase(AddressingMode.AbsoluteY, RegisterName.Y)]
    public void ForAbsoluteIndexedMode(AddressingMode mode, RegisterName register)
    {
      memory.SetValue(0x1010, 0x8);
      model.GetRegister(register).SetValue(0x10);
      Assert.That(ArgumentUtils.ArgumentFor(model, memory, mode, 0x1000).value,
                  Is.EqualTo(0x8));
    }

    [Test]
    public void UsesTheOperandAsTheEffectiveAddressForZeropageMode()
    {
      memory.SetValue(0x0C0, 0x8);
      Assert.That(ArgumentUtils.ArgumentFor(model, memory, AddressingMode.Zeropage,
                                            0xC0).value, Is.EqualTo(0x8));
    }

    [TestCase(AddressingMode.ZeropageX, RegisterName.X)]
    [TestCase(AddressingMode.ZeropageY, RegisterName.Y)]
    public void ForZeropageIndexedMode(AddressingMode mode, RegisterName register)
    {
      memory.SetValue(0x0070, 0x8);
      model.GetRegister(register).SetValue(0x10);
      Assert.That(ArgumentUtils.ArgumentFor(model, memory, mode, 0x60).value,
                  Is.EqualTo(0x8));
    }

    [TestCase(AddressingMode.IndirectX, RegisterName.X)]
    [TestCase(AddressingMode.IndirectY, RegisterName.Y)]
    public void ForIndirectMode(AddressingMode mode, RegisterName register)
    {
      memory.SetValue(0x8000, 0x8);
      memory.SetValue(0x0070, 0x00);
      memory.SetValue(0x0071, 0x80);
      model.GetRegister(register).SetValue(0x10);
      Assert.That(ArgumentUtils.ArgumentFor(model, memory, mode, 0x60).value,
                  Is.EqualTo(0x8));
    }

    [TestCase(AddressingMode.AbsoluteX, RegisterName.X)]
    [TestCase(AddressingMode.AbsoluteY, RegisterName.Y)]
    public void IncludesTheAddressInTheArgument(AddressingMode mode,
        RegisterName register)
    {
      memory.SetValue(0x1010, 0x8);
      model.GetRegister(register).SetValue(0x10);
      Assert.That(ArgumentUtils.ArgumentFor(model, memory, mode, 0x1000).address,
                  Is.EqualTo(0x1010));
    }

    [TestCase(AddressingMode.AbsoluteX, RegisterName.X)]
    [TestCase(AddressingMode.AbsoluteY, RegisterName.Y)]
    public void ReturnsTrueWhenAPageBoundaryWasCrossed(AddressingMode mode,
        RegisterName register)
    {
      model.GetRegister(register).SetValue(0x10);
      Assert.That(ArgumentUtils.CrossesPageBoundary(model, mode, 0x10F0),
                  Is.True, "A page boundary was crossed, but not noticed.");
    }

    [TestCase(AddressingMode.AbsoluteX, RegisterName.X)]
    [TestCase(AddressingMode.AbsoluteY, RegisterName.Y)]
    [TestCase(AddressingMode.Immediate, RegisterName.Y)]
    [TestCase(AddressingMode.Absolute, RegisterName.Y)]
    [TestCase(AddressingMode.Implied, RegisterName.Y)]
    [TestCase(AddressingMode.IndirectX, RegisterName.Y)]
    [TestCase(AddressingMode.IndirectY, RegisterName.Y)]
    [TestCase(AddressingMode.Zeropage, RegisterName.Y)]
    [TestCase(AddressingMode.ZeropageX, RegisterName.Y)]
    [TestCase(AddressingMode.ZeropageY, RegisterName.Y)]
    public void ReturnsFalseWhenAPageBoundaryWasNotCrossed(AddressingMode mode,
        RegisterName register)
    {
      model.GetRegister(register).SetValue(0x10);
      Assert.That(ArgumentUtils.CrossesPageBoundary(model, mode, 0x10E0), Is.False,
                  "We reported incorrectly that a page boundary was crossed.");
    }

    [Test]
    public void ReturnsAnAccumulatorArgumentForAccumulatorMode()
    {
      Assert.IsInstanceOfType(typeof(AccumulatorArgument),
                              ArgumentUtils.ArgumentFor(model, memory,
                                  AddressingMode.Accumulator, 0));
    }
  }
}
