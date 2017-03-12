using Mos6510.Instructions;
using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class DecoderTests
  {
    [TestCase(0xE0, Opcode.Inx, AddressingMode.Implied, 3)]
    [TestCase(0xC0, Opcode.Iny, AddressingMode.Implied, 4)]
    public void FindsTheProperOpCodeAndAddressingModeForTheInstruction(
      byte instruction, Opcode expectedOpcode,
      AddressingMode expectedAddressingMode,
      int expectedCycles)
    {
      var registry = new Registry {
        { instruction,
          expectedOpcode,
          null,
          expectedAddressingMode,
          expectedCycles,
          0
        }
      };

      var decoder = new Decoder(registry);
      OpcodeData data;
      var hasInstruction = decoder.TryDecode(instruction, out data);

      Assert.That(hasInstruction, Is.True, @"Try Decode returned false for a
          valid instruction, which is not expected.");
      Assert.That(data.Opcode, Is.EqualTo(expectedOpcode));
      Assert.That(data.Mode, Is.EqualTo(expectedAddressingMode));
      Assert.That(data.Cycles, Is.EqualTo(expectedCycles));
    }

    [Test]
    public void ReturnsFalseIfTheInstructionIsNotValid()
    {
      var decoder = new Decoder(new Registry());
      OpcodeData data;
      Assert.That(decoder.TryDecode(0xFF, out data), Is.False, @"TryDecode returned
          true for an invalid instruction, which is not expected.");
    }
  }
}
