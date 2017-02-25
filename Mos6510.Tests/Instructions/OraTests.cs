using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests.Instructions
{
[TestFixture]
public class OraTests
{
    [Test]
    public void OrsWithTheAccumulator()
    {
        var model = new ProgrammingModel();
        var accumulator = model.GetRegister(RegisterName.A);
        accumulator.SetValue(0xA);

        new Ora().Execute(model, null, new Argument(0x1,0));
        Assert.That(accumulator.GetValue(), Is.EqualTo(0xB));
    }

    [TestCase(AddressingMode.Immediate, 2)]
    [TestCase(AddressingMode.Absolute, 4)]
    [TestCase(AddressingMode.AbsoluteX, 4)]
    [TestCase(AddressingMode.AbsoluteY, 4)]
    [TestCase(AddressingMode.Zeropage, 3)]
    [TestCase(AddressingMode.ZeropageX, 4)]
    [TestCase(AddressingMode.ZeropageY, 4)]
    [TestCase(AddressingMode.IndexedIndirectX, 6)]
    [TestCase(AddressingMode.IndexedIndirectY, 5)]
    public void ReturnsTheProperNumberOfCycles(AddressingMode mode, int expected)
    {
        Assert.That(new Ora().CyclesFor(mode), Is.EqualTo(expected));
    }

    [TestCase(0x80, true)]
    [TestCase(0x00, false)]
    public void VerifyValuesOfNegativeFlag(int initialValue, bool expectedResult)
    {
        var model = new ProgrammingModel();
        model.GetRegister(RegisterName.A).SetValue(initialValue);
        model.NegativeFlag = !expectedResult;

        new Ora().Execute(model, null, new Argument(0x7F,0));

        Assert.That(model.NegativeFlag, Is.EqualTo(expectedResult));
    }

    [TestCase(0x00, true)]
    [TestCase(0x01, false)]
    public void VerifyValuesOfZeroFlag(int initialValue, bool expectedResult)
    {
        var model = new ProgrammingModel();
        model.GetRegister(RegisterName.A).SetValue(initialValue);
        model.ZeroFlag = !expectedResult;

        new Ora().Execute(model, null, new Argument(0x00,0));

        Assert.That(model.ZeroFlag, Is.EqualTo(expectedResult));
    }
}
}
