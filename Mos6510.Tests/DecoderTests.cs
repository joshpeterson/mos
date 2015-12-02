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
      OpcodeAddressModePair pair;
      var hasInstruction = decoder.TryDecode(instruction, out pair);
      Assert.That(hasInstruction, Is.True, @"Try Decode returned false for a
          valid instruction, which is not expected.");
      Assert.That(pair.Opcode, Is.EqualTo(expectedOpcode));
      Assert.That(pair.Mode, Is.EqualTo(expectedAddressingMode));
    }

    [Test]
    public void ReturnsFalseIfTheInstructionIsNotValid()
    {
      var decoder = new Decoder();
      OpcodeAddressModePair pair;
      Assert.That(decoder.TryDecode(0xFF, out pair), Is.False, @"TryDecode returned
          true for an invalid instruction, which is not expected.");
    }
  }
}
