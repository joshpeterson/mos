using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests
{
[TestFixture]
public class FetcherTests
{
    private ProgrammingModel model;
    private Memory memory;
    private Fetcher fetcher;

    [SetUp]
    public void SetUp()
    {
        model = new ProgrammingModel();
        memory = new Memory();
        fetcher = new Fetcher(model, memory);
    }

    [Test]
    public void FetchesTheNextInstructionFromTheProgramCounter()
    {
        const ushort expectedAddress = 0xFF10;
        const byte expectedValue = 0x2A;

        model.GetRegister(RegisterName.PC).SetValue(expectedAddress);
        memory.SetValue(expectedAddress, expectedValue);

        Assert.That(fetcher.Fetch(), Is.EqualTo(expectedValue));
    }

    [Test]
    public void IncrementsThePCOneByte()
    {
        const ushort initialAddress = 0xFF10;

        model.GetRegister(RegisterName.PC).SetValue(initialAddress);

        fetcher.Fetch();
        Assert.That(model.GetRegister(RegisterName.PC).GetValue(),
                    Is.EqualTo(initialAddress + 1));
    }

    [TestCase(AddressingMode.Immediate)]
    [TestCase(AddressingMode.Zeropage)]
    [TestCase(AddressingMode.ZeropageX)]
    [TestCase(AddressingMode.ZeropageY)]
    [TestCase(AddressingMode.IndexedIndirectX)]
    [TestCase(AddressingMode.IndexedIndirectY)]
    public void GetsOneByteTheOperandFor(AddressingMode mode)
    {
        const ushort address = 0xFF10;
        const byte expectedOperand = 0xC2;

        model.GetRegister(RegisterName.PC).SetValue(address);
        memory.SetValue(address + 1, expectedOperand);
        fetcher.Fetch();

        Assert.That(fetcher.OperandFor(mode), Is.EqualTo(expectedOperand));
    }

    [TestCase(AddressingMode.Immediate)]
    [TestCase(AddressingMode.Zeropage)]
    [TestCase(AddressingMode.ZeropageX)]
    [TestCase(AddressingMode.ZeropageY)]
    [TestCase(AddressingMode.IndexedIndirectX)]
    [TestCase(AddressingMode.IndexedIndirectY)]
    public void IncrementsThePCAferReadingAOneByteOperand(AddressingMode mode)
    {
        const ushort initialAddress = 0xFF10;

        model.GetRegister(RegisterName.PC).SetValue(initialAddress);
        fetcher.Fetch();

        fetcher.OperandFor(mode);
        Assert.That(model.GetRegister(RegisterName.PC).GetValue(),
                    Is.EqualTo(initialAddress + 2));
    }

    [TestCase(AddressingMode.Absolute)]
    [TestCase(AddressingMode.AbsoluteX)]
    [TestCase(AddressingMode.AbsoluteY)]
    public void GetsTwoByteTheOperandFor(AddressingMode mode)
    {
        const ushort address = 0xFF10;
        const byte operandLow = 0xC2;
        const byte operandHigh = 0x59;
        ushort expectedOperand = (operandHigh << 8) | operandLow;

        model.GetRegister(RegisterName.PC).SetValue(address);
        memory.SetValue(address + 1, operandLow);
        memory.SetValue(address + 2, operandHigh);
        fetcher.Fetch();

        Assert.That(fetcher.OperandFor(mode), Is.EqualTo(expectedOperand));
    }

    [TestCase(AddressingMode.Absolute)]
    [TestCase(AddressingMode.AbsoluteX)]
    [TestCase(AddressingMode.AbsoluteY)]
    public void IncrementsThePCAferReadingATwoByteOperand(AddressingMode mode)
    {
        const ushort initialAddress = 0xFF10;

        model.GetRegister(RegisterName.PC).SetValue(initialAddress);
        fetcher.Fetch();

        fetcher.OperandFor(mode);
        Assert.That(model.GetRegister(RegisterName.PC).GetValue(),
                    Is.EqualTo(initialAddress + 3));
    }
}
}
