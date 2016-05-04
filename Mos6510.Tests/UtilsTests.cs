using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class UtilsTests
  {
    [Test]
    public void TryParseOpcodeReturnsTrueIfAnOpcodeIsCorrectlyParsed()
    {
      Opcode unused;
      Assert.That(Utils.TryParseOpcode("Nop", out unused), Is.True,
          @"TryParseOpcode returned false for a valid opcode,
            which is not expected.");
    }

    [Test]
    public void TryParseOpcodeReturnsFalseIfAnOpcodeIsCorrectlyParsed()
    {
      Opcode unused;
      Assert.That(Utils.TryParseOpcode("Foo", out unused), Is.False,
          @"TryParseOpcode returned true for an invalid opcode,
            which is not expected.");
    }

    [Test]
    public void TryParseOpcodeSetsTheOpcodeForAValidInput()
    {
      Opcode actualOpcode;
      Utils.TryParseOpcode("Inx", out actualOpcode);
      Assert.That(actualOpcode, Is.EqualTo(Opcode.Inx));
    }
  }
}
