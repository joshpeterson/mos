using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class ReplTests
  {
    [Test]
    public void CanPrintTheDefaultContentsOfAllRegisters()
    {
      const string expectedRegisters = @"
        Registers:
        A:  0x00
        Y:  0x00
        X:  0x00
        PC: 0x0000
        S:  0x00
        P:  00100000b
            NVXBDIZC";

      Assert.That(new Repl(new ProgrammingModel(), new Memory()).PrintRegisters(),
                  Is.EqualTo(expectedRegisters));
    }

    [TestCase(RegisterName.A, 42, "A:  0x2A")]
    [TestCase(RegisterName.Y, 42, "Y:  0x2A")]
    [TestCase(RegisterName.X, 42, "X:  0x2A")]
    [TestCase(RegisterName.PC, 8023, "PC: 0x1F57")]
    [TestCase(RegisterName.S, 42, "S:  0x2A")]
    [TestCase(RegisterName.P, 142, "P:  10001110b")]
    public void CanPrintTheContentsOfRegister(RegisterName register, int value,
        string expectedValue)
    {
      var model = new ProgrammingModel();
      model.GetRegister(register).SetValue(value);
      var repl = new Repl(model, new Memory());

      Assert.That(repl.PrintRegisters(), Is.StringContaining(expectedValue));
    }

    [Test]
    public void CanReadAnInstructionAndInsertItIntoMemoryAtThePCLocation()
    {
      const ushort pcValue = 0xFFE0;
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.PC).SetValue(pcValue);

      var memory = new Memory();
      new Repl(model, memory).TryRead("Inx");
      Assert.That(memory.GetValue(pcValue), Is.EqualTo(0xE0));
    }

    [Test]
    public void TryReadReturnsFalseForAnInvalidInputString()
    {
      Assert.That(new Repl(new ProgrammingModel(), new Memory()).TryRead("foo"),
                  Is.False, "TryRead returned true, which is not expected.");
    }

    [Test]
    public void TryReadReturnsTrueForAValidInputString()
    {
      Assert.That(new Repl(new ProgrammingModel(), new Memory()).TryRead("Inx"),
                  Is.True, "TryRead returned false, which is not expected.");
    }

    [Test]
    public void CanExecuteTheInstructionAtThePCLocation()
    {
      const ushort pcValue = 0xFFE0;
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.PC).SetValue(pcValue);

      var memory = new Memory();
      var repl = new Repl(model, memory);
      repl.TryRead("Inx");
      repl.Execute();
      Assert.That(model.GetRegister(RegisterName.X).GetValue(), Is.EqualTo(1));
    }

    [Test]
    public void WriteTheArgumentForATwoByteInstruction()
    {
      const ushort pcInitialValue = 0xFFE0;
      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.PC).SetValue(pcInitialValue);

      var memory = new Memory();
      new Repl(model, memory).TryRead("And #$40");
      Assert.That(memory.GetValue(pcInitialValue + 1), Is.EqualTo(0x40));
    }
  }
}
