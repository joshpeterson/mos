using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class ExecutorTests
  {
    private const Opcode opcode = Opcode.Nop;
    private InstructionTestDouble instruction;
    private ProgrammingModel model;
    private Memory memory;
    private Executor executor;

    [SetUp]
    public void SetUp()
    {
      instruction = new InstructionTestDouble();
      var registry = new Registry {
        { 0x00, opcode, instruction, AddressingMode.Implied} };

      model = new ProgrammingModel();
      memory = new Memory();
      executor = new Executor(registry, model, memory);
    }

    [Test]
    public void ExecutesAGivenInstruction()
    {
      executor.Execute(opcode, AddressingMode.Implied, 0);
      Assert.That(instruction.ExecuteCalled, Is.True, @"The Execute method was not
          called on the instruction, which is not expected.");
    }

    [Test]
    public void PassesTheArgumentAsZeroForImpliedMode()
    {
      executor.Execute(opcode, AddressingMode.Implied, 42);
      Assert.That(instruction.ProvidedArgument, Is.EqualTo(0));
    }

    [Test]
    public void PassesTheArgumentBasedOnTheAddressingMode()
    {
      const ushort address = 0x2000;
      const byte expectedArgument = 0xF2;
      memory.SetValue(address, expectedArgument);

      executor.Execute(opcode, AddressingMode.Absolute, address);
      Assert.That(instruction.ProvidedArgument, Is.EqualTo(expectedArgument));
    }

    [Test]
    public void ReturnsTheNumberOfCyclesForTheInstruction()
    {
      Assert.That(executor.Execute(opcode, AddressingMode.Absolute, 0),
                  Is.EqualTo(InstructionTestDouble.NumberOfCycles));
    }

    [TestCase(AddressingMode.AbsoluteX, RegisterName.X)]
    [TestCase(AddressingMode.AbsoluteY, RegisterName.Y)]
    public void ReturnsTheProperNumberOfCyclesWhenCrossingPageBoundary(
        AddressingMode mode, RegisterName register)
    {
      model.GetRegister(register).SetValue(0x10);
      Assert.That(executor.Execute(opcode, mode, 0x10F0),
                  Is.EqualTo(InstructionTestDouble.NumberOfCycles + 1));
    }

    [TestCase(AddressingMode.AbsoluteX, RegisterName.X)]
    [TestCase(AddressingMode.AbsoluteY, RegisterName.Y)]
    public void ReturnsTheProperNumberOfCyclesWhenNotCrossingPageBoundary(
        AddressingMode mode, RegisterName register)
    {
      model.GetRegister(register).SetValue(0x10);
      Assert.That(executor.Execute(opcode, mode, 0x10E0),
                  Is.EqualTo(InstructionTestDouble.NumberOfCycles));
    }

    public class InstructionTestDouble : Instruction
    {
      public const int NumberOfCycles = 42;
      public bool ExecuteCalled { get; private set; }
      public byte ProvidedArgument {get; private set; }

      public virtual void Execute(ProgrammingModel model, Memory memory,
                                  byte argument)
      {
        ExecuteCalled = true;
        ProvidedArgument = (byte)argument;
      }

      public virtual int CyclesFor(AddressingMode mode)
      {
        return NumberOfCycles;
      }
    }
  }
}
