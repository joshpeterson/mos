using NUnit.Framework;
using Mos6510;
using Mos6510.Instructions;

namespace Mos6510.Tests
{
  [TestFixture]
  public class ExecutorTests
  {
    private const Opcode opcode = Opcode.Nop;
    private InstructionTestDouble instruction;
    private Memory memory;
    private Executor executor;

    [SetUp]
    public void SetUp()
    {
      instruction = new InstructionTestDouble();
      var registry = new Registry {
        { 0x00, opcode, instruction, AddressingMode.Implied} };

      memory = new Memory();
      executor = new Executor(registry, new ProgrammingModel(), memory);
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

    public class InstructionTestDouble : Instruction
    {
      public bool ExecuteCalled { get; private set; }
      public byte ProvidedArgument {get; private set; }

      public virtual void Execute(ProgrammingModel model, Memory memory, byte argument)
      {
        ExecuteCalled = true;
        ProvidedArgument = (byte)argument;
      }

      public virtual int CyclesFor(ProgrammingModel model, AddressingMode mode,
                                    ushort operand)
      {
        return 0;
      }
    }
  }
}
