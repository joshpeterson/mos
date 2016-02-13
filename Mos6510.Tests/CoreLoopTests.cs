using NUnit.Framework;
using Mos6510;
using Mos6510.Instructions;

namespace Mos6510.Tests
{
  [TestFixture]
  public class CoreLoopTests
  {
    private const ushort InstructionAddress = 0xFF10;
    private Memory memory;
    private CoreLoop loop;
    private InstructionTestDouble instruction;

    [SetUp]
    public void SetUp()
    {
      const byte expectedCode = 0xEA; // Nop opcode

      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.PC).SetValue(InstructionAddress);

      memory = new Memory();
      memory.SetValue(InstructionAddress, expectedCode);

      instruction = new InstructionTestDouble();
      var registry = new Registry {
        { expectedCode, Opcode.Nop, instruction, AddressingMode.Implied } };

      var fetcher = new Fetcher(model, memory);
      var decoder = new Decoder(registry);
      var executor = new Executor(registry, model, memory);

      loop = new CoreLoop(fetcher, decoder, executor);
    }

    [Test]
    public void SingleStepExecutesOneInstruction()
    {
      loop.SingleStep();

      Assert.That(instruction.ExecuteCalled, Is.True, @"The Execute method was not
          called on the instruction, which is not expected.");
    }

    [Test]
    public void SingleStepReturnsFalseForAnInvalidInstruction()
    {
      memory.SetValue(InstructionAddress, 0xFF); //Invalid instruction
      Assert.That(loop.SingleStep(), Is.False,@"The SingleStep method returned
          true for an invalid instruction, which is not expected.");

    }

    private class InstructionTestDouble : Instruction
    {
      public bool ExecuteCalled {get; private set; }

      public int Execute(ProgrammingModel model, Memory memory,
                          AddressingMode mode, ushort operand)
      {
        ExecuteCalled = true;
        return 0;
      }
    }
  }
}
