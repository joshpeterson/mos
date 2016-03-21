using NUnit.Framework;
using Mos6510;
using Mos6510.Instructions;
using System.Linq;

namespace Mos6510.Tests
{
  [TestFixture]
  public class AssemblerTests
  {
    private const byte code = 0xFF;
    private Assembler assembler;

    [SetUp]
    public void SetUp()
    {
      var registry = new Registry {
        { code, Opcode.Nop, null, AddressingMode.Implied } };
      assembler = new Assembler(registry);
    }

    [Test]
    public void CanConvertAStringToAByteCode()
    {
      Assert.That(assembler.GetDisassembly("Nop").First(), Is.EqualTo(code));
    }

    [Test]
    public void CanReadOneByteHexadecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("Nop #$40"),
                  Is.EquivalentTo(new [] { code, 0x40 }));
    }

    [Test]
    public void CanReadOneByteDecimalOperand()
    {
      Assert.That(assembler.GetDisassembly("Nop #40"),
                  Is.EquivalentTo(new [] { code, 40 }));
    }
  }
}
