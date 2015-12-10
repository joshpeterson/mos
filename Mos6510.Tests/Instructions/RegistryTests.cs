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
        { 0x00, opcode, instruction, AddressingMode.Implied } };

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
        { code, opcode, instruction, AddressingMode.Implied } };

      Assert.That(registry.Get(code).Opcode, Is.EqualTo(opcode));
      Assert.That(registry.Get(code).Mode, Is.EqualTo(mode));
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
  }

  public class InstructionTestDouble : Instruction
  {
    public void Execute(ProgrammingModel model)
    {
    }
  }
}
