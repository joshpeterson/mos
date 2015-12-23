using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class IntegrationTests
  {
    [TestCase("Inx", "X:  0x01")]
    [TestCase("Iny", "Y:  0x01")]
    public void InstructionWorksEndToEnd(string input, string expectedOutput)
    {
      var repl = new Repl(new ProgrammingModel(), new Memory());

      repl.TryRead(input);
      repl.Execute();
      Assert.That(repl.PrintRegisters(), Is.StringContaining(expectedOutput));
    }
  }
}
