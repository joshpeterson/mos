using NUnit.Framework;
using Mos6510;

namespace Mos6510.Tests
{
  [TestFixture]
  public class ReplTests
  {
    [Test]
    public void CanPrintTheDefaultContentsOfAllRegisters()
    {
      var repl = new Repl(new ProgrammingModel(), new Memory());

      const string expectedRegisters = @"
        Registers:
        A:  0x00
        Y:  0x00
        X:  0x00
        PC: 0x0000
        S:  0x00
        P:  00000000b
            NVXBDIZC";

      Assert.That(repl.PrintRegisters(), Is.EqualTo(expectedRegisters));
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
  }
}
