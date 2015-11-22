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
      var instruction = new InstructionTestDouble();
      var registry = new Registry { { Opcode.Nop, instruction } };
      Assert.That(registry.Get(Opcode.Nop), Is.EqualTo(instruction));
    }

    [Test]
    public void RegistryReturnsNullForAnUnknownOpcode()
    {
      var registry = new Registry();
      Assert.That(registry.Get(Opcode.Nop), Is.Null);
    }
  }

  public class InstructionTestDouble : Instruction
  {
    public void Execute(ProgrammingModel model)
    {
    }
  }
}
