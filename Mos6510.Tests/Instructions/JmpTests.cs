using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class JmpTests
  {
    [Test]
    public void SetsTheProgramCounter()
    {
      const ushort address = 0x2620;
      var model = new ProgrammingModel();
      var argument = new Argument(0, address);

      new Jmp().Execute(model, new Memory(), argument);

      Assert.That(model.GetRegister(RegisterName.PC).GetValue(),
                  Is.EqualTo(address));
    }
  }
}
