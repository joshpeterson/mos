using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests
{
  [TestFixture]
  public class DecoderTests
  {
    [TestCase(0xE0, Opcode.Inx, AddressingMode.Implied)]
    [TestCase(0xC0, Opcode.Iny, AddressingMode.Implied)]
    public void FindsTheProperOpCodeAndAddressingModeForTheInstruction(byte instruction,
        Opcode expectedOpcode, AddressingMode expectedAddressingMode)
    {
      var decoder = new Decoder();
      var pair = decoder.Decode(instruction);
      Assert.That(pair.Opcode, Is.EqualTo(expectedOpcode));
      Assert.That(pair.AddressingMode, Is.EqualTo(expectedAddressingMode));
    }
  }
}
