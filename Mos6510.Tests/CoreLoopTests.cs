using Mos6510.Instructions;
using NUnit.Framework;

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
            { expectedCode, Opcode.Nop, instruction, AddressingMode.Absolute }
        };

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

    [Test]
    public void ReadsOperandAndPassesItToTheInstruction()
    {
        const byte expectedArgument = 0x2A;
        memory.SetValue(0x1004, expectedArgument);
        memory.SetValue(InstructionAddress + 1, 0x04);
        memory.SetValue(InstructionAddress + 2, 0x10);

        loop.SingleStep();
        Assert.That(instruction.ArgumentUsed.value, Is.EqualTo(expectedArgument));
    }

    private class InstructionTestDouble : Instruction
    {
        public bool ExecuteCalled {
            get;
            private set;
        }
        public Argument ArgumentUsed {
            get;
            private set;
        }

        public virtual void Execute(ProgrammingModel model, Memory memory,
                                    Argument argument)
        {
            ExecuteCalled = true;
            ArgumentUsed = argument;
        }

        public virtual int CyclesFor(AddressingMode mode)
        {
            return 0;
        }
    }
}
}
