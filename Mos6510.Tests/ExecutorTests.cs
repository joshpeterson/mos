using NUnit.Framework;
using Mos6510;
using Mos6510.Instructions;

namespace Mos6510.Tests
{
  [TestFixture]
  public class ExecutorTests
  {
    [Test]
    public void ExecutesAGivenInstruction()
    {
      const Opcode opcode = Opcode.Nop;

      var instruction = new InstructionTestDouble();
      var registry = new Registry { { opcode, instruction } };

      var model = new ProgrammingModel();

      var memory = new Memory();

      var executor = new Executor(registry, model, memory);
      executor.Execute(opcode, AddressingMode.Implied);

      Assert.That(instruction.ExecuteCalled, Is.True, @"The Execute method was not
          called on the instruction, which is not expected.");
    }

    public class InstructionTestDouble : Instruction
    {
      public bool ExecuteCalled {get; private set; }

      public void Execute(ProgrammingModel model)
      {
        ExecuteCalled = true;
      }
    }
  }
}
