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
    private Assembler assembler;

    [SetUp]
    public void SetUp()
    {
      var registry = new Registry {
        { nopCode, Opcode.Nop, null, AddressingMode.Implied },
        { andImmediateCode, Opcode.And, null, AddressingMode.Immediate },
      };
      assembler = new Assembler(registry);
    }

    [Test]
    public void CanConvertAStringToAByteCode()
    {
      Assert.That(assembler.GetDisassembly("Nop").First(), Is.EqualTo(nopCode));
    }

    [Test]
    public void CanReadOneByteHexadecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("And #$40"),
                  Is.EquivalentTo(new [] { andImmediateCode, 0x40 }));
    }

    [Test]
    public void CanReadOneByteDecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("And #40"),
                  Is.EquivalentTo(new [] { andImmediateCode, 40 }));
    }
  }
}
