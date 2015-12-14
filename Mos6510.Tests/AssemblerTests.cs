using NUnit.Framework;
using Mos6510;
using Mos6510.Instructions;

namespace Mos6510.Tests
{
  [TestFixture]
  public class AssemblerTests
  {
    [Test]
    public void CanConvertAStringToAByteCode()
    {
      const byte code = 0xFF;
      const Opcode opcode = Opcode.Nop;

      var registry = new Registry {
        { code, opcode, null, AddressingMode.Implied } };
      var assembler = new Assembler(registry);

      Assert.That(assembler.GetByteCode("Nop"), Is.EqualTo(code));
    }
  }
}
