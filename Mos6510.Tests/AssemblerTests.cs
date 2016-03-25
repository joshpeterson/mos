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
    public void CanConvertAStringToAByteCode()
    {
      Assert.That(assembler.GetDisassembly("Nop").First(), Is.EqualTo(nopCode));
    }

    [Test]
    public void CanReadImmediateModeHexadecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("And #$40"),
                  Is.EquivalentTo(new [] { andImmediateCode, 0x40 }));
    }

    [Test]
    public void CanReadImmediateModeDecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("And #40"),
                  Is.EquivalentTo(new [] { andImmediateCode, 40 }));
    }

    [Test]
    public void CanReadAbsoluteModeHexadecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("And $1040"),
                  Is.EquivalentTo(new [] { andAbsoluteCode, 0x40, 0x10 }));
    }

    [Test]
    public void CanReadAbsoluteModDecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("And 4160"),
                  Is.EquivalentTo(new [] { andAbsoluteCode, 0x40, 0x10 }));
    }

    [Test]
    public void CanReadAbsoluteXModeHexadecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("And $1040,X"),
                  Is.EquivalentTo(new [] { andAbsoluteXCode, 0x40, 0x10 }));
    }

    [Test]
    public void CanReadAbsoluteXModeDecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("And 4160,X"),
                  Is.EquivalentTo(new [] { andAbsoluteXCode, 0x40, 0x10 }));
    }
  }
}
