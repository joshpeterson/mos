using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class RegistryTests
  {
    [Test]
    public void CanInitializeRegistryWithAnInstruction()
    {
      const Opcode opcode = Opcode.Nop;

      var instruction = new InstructionTestDouble();
      var registry = new Registry {
        { 0x00, opcode, instruction, AddressingMode.Implied }
      };

      Assert.That(registry.Get(opcode), Is.EqualTo(instruction));
    }

    [Test]
    public void CanInitializeRegistryWithTwoInstructionsUsingTheSameOpcode()
    {
      const Opcode opcode = Opcode.Nop;

      var instruction = new InstructionTestDouble();
      var registry = new Registry {
        { 0x00, opcode, instruction, AddressingMode.Implied },
        { 0x10, opcode, instruction, AddressingMode.Immediate }
      };

      Assert.That(registry.Get(opcode), Is.EqualTo(instruction));
    }

    [Test]
    public void CanInitializeRegistryWithACodeAddressingModePair()
    {
      const byte code = 0xFF;
      const Opcode opcode = Opcode.Nop;
      const AddressingMode mode = AddressingMode.Implied;

      var instruction = new InstructionTestDouble();
      var registry = new Registry {
        { code, opcode, instruction, AddressingMode.Implied }
      };

      Assert.That(registry.Get(code).Opcode, Is.EqualTo(opcode));
      Assert.That(registry.Get(code).Mode, Is.EqualTo(mode));
    }

    [Test]
    public void CanGetACodeFromAnInstructionOpcodePair()
    {
      const byte expectedCode = 0xFF;
      const Opcode opcode = Opcode.Nop;
      const AddressingMode mode = AddressingMode.Implied;

      var instruction = new InstructionTestDouble();
      var registry = new Registry {
        { expectedCode, opcode, instruction, AddressingMode.Implied }
      };

      Assert.That(registry.Get(opcode, mode), Is.EqualTo(expectedCode));
    }

    [Test]
    public void RegistryReturnsNullForAnUnknownOpcode()
    {
      var registry = new Registry();
      Assert.That(registry.Get(Opcode.Nop), Is.Null);
    }

    [Test]
    public void RegistryReturnsNullForAnUnknownCode()
    {
      var registry = new Registry();
      Assert.That(registry.Get(0xFF), Is.Null);
    }

    [Test]
    public void RegistryReturnsZeroForAnUnknownOpcodeAndAddresModePair()
    {
      var registry = new Registry();
      Assert.That(registry.Get(Opcode.Nop, AddressingMode.Implied), Is.EqualTo(0));
    }
  }

  public class InstructionTestDouble : Instruction
  {
    public virtual void Execute(ProgrammingModel model, Memory memory,
                                byte argument)
    {
    }

    public virtual int CyclesFor(AddressingMode mode)
    {
      return 0;
    }
  }
}
