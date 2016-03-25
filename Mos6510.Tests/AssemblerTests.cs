using NUnit.Framework;
using Mos6510;
using Mos6510.Instructions;
using System.Linq;

namespace Mos6510.Tests
{
  [TestFixture]
  public class AssemblerTests
  {
    private const byte nopCode = 0xFF;
    private const byte andImmediateCode = 0x29;
    private const byte andAbsoluteCode = 0x2D;
    private const byte andAbsoluteXCode = 0x3D;
    private const byte andAbsoluteYCode = 0x39;
    private const byte andZeropageCode = 0x25;
    private const byte andZeropageXCode = 0x35;
    private const byte andZeropageYCode = 0x45;
    private const byte andIndexedIndirectXCode = 0x21;
    private const byte andIndexedIndirectYCode = 0x31;
    private Assembler assembler;

    [SetUp]
    public void SetUp()
    {
      var registry = new Registry {
        { nopCode, Opcode.Nop, null, AddressingMode.Implied },
        { andImmediateCode, Opcode.And, null, AddressingMode.Immediate },
        { andAbsoluteCode, Opcode.And, null, AddressingMode.Absolute },
        { andAbsoluteXCode, Opcode.And, null, AddressingMode.AbsoluteX },
        { andAbsoluteYCode, Opcode.And, null, AddressingMode.AbsoluteY },
        { andZeropageCode, Opcode.And, null, AddressingMode.Zeropage },
        { andZeropageXCode, Opcode.And, null, AddressingMode.ZeropageX },
        { andZeropageYCode, Opcode.And, null, AddressingMode.ZeropageY },
        { andIndexedIndirectXCode, Opcode.And, null, AddressingMode.IndexedIndirectX},
        { andIndexedIndirectYCode, Opcode.And, null, AddressingMode.IndexedIndirectY},
      };
      assembler = new Assembler(registry);
    }

    [TestCase("Nop", nopCode)]
    [TestCase("NOP", nopCode)]
    [TestCase("nop", nopCode)]
    [TestCase("noP", nopCode)]
    public void CanReadNoArgumentInstruction(string input, byte code)
    {
      Assert.That(assembler.GetDisassembly(input).First(), Is.EqualTo(code));
    }

    [TestCase("And #$40", andImmediateCode, 0x40)]
    [TestCase("And #40", andImmediateCode, 0x28)]
    [TestCase("And $80", andZeropageCode, 0x80 )]
    [TestCase("And 64", andZeropageCode, 0x40 )]
    [TestCase("And $80,X", andZeropageXCode, 0x80 )]
    [TestCase("And $80,x", andZeropageXCode, 0x80 )]
    [TestCase("And 64,X", andZeropageXCode, 0x40 )]
    [TestCase("And $80,Y", andZeropageYCode, 0x80 )]
    [TestCase("And $80,y", andZeropageYCode, 0x80 )]
    [TestCase("And 64,Y", andZeropageYCode, 0x40 )]
    [TestCase("And ($80,X)", andIndexedIndirectXCode, 0x80 )]
    [TestCase("And (64,X)", andIndexedIndirectXCode, 0x40 )]
    [TestCase("And ($80,x)", andIndexedIndirectXCode, 0x80 )]
    [TestCase("And ($80),Y", andIndexedIndirectYCode, 0x80 )]
    [TestCase("And (64),Y", andIndexedIndirectYCode, 0x40 )]
    [TestCase("And ($80),y", andIndexedIndirectYCode, 0x80 )]
    public void CanReadOneArgumentInstruction(string input, byte code,
                                              byte argument)
    {
      Assert.That(assembler.GetDisassembly(input),
                  Is.EquivalentTo(new [] { code, argument }));
    }

    [TestCase("And $1040", andAbsoluteCode, 0x40, 0x10 )]
    [TestCase("And 4160", andAbsoluteCode, 0x40, 0x10 )]
    [TestCase("And $1040,X", andAbsoluteXCode, 0x40, 0x10 )]
    [TestCase("And 4160,X", andAbsoluteXCode, 0x40, 0x10 )]
    [TestCase("And $1040,x", andAbsoluteXCode, 0x40, 0x10 )]
    [TestCase("And $1040,Y", andAbsoluteYCode, 0x40, 0x10 )]
    [TestCase("And 4160,Y", andAbsoluteYCode, 0x40, 0x10 )]
    [TestCase("And $1040,y", andAbsoluteYCode, 0x40, 0x10 )]
    public void CanReadTwoArgumentInstruction(string input, byte code,
                                              byte argument1, byte argument2)
    {
      Assert.That(assembler.GetDisassembly(input),
                  Is.EquivalentTo(new [] { code, argument1, argument2 }));
    }
  }
}
