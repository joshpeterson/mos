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
    private Assembler assembler;

    [SetUp]
    public void SetUp()
    {
      var registry = new Registry {
        { nopCode, Opcode.Nop, null, AddressingMode.Implied },
        { andImmediateCode, Opcode.And, null, AddressingMode.Immediate },
        { andAbsoluteCode, Opcode.And, null, AddressingMode.Absolute },
        { andAbsoluteXCode, Opcode.And, null, AddressingMode.AbsoluteX },
      };
      assembler = new Assembler(registry);
    }

    [Test]
    public void CanReadNoArgumentInstruction()
    {
      Assert.That(assembler.GetDisassembly("Nop").First(), Is.EqualTo(nopCode));
    }

    [TestCase("And #$40", andImmediateCode, 0x40)]
    [TestCase("And #40", andImmediateCode, 0x28)]
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
    public void CanReadTwoArgumentInstruction(string input, byte code,
                                              byte argument1, byte argument2)
    {
      Assert.That(assembler.GetDisassembly(input),
                  Is.EquivalentTo(new [] { code, argument1, argument2 }));
    }
  }
}
