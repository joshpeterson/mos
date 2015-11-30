using NUnit.Framework;
using Mos6510;

namespace Mos6510.Tests
{
  [TestFixture]
  public class FetcherTests
  {
    [Test]
    public void FetchesTheNextInstructionFromTheProgramCounter()
    {
      const ushort expectedAddress = 0xFF10;
      const byte expectedValue = 0x2A;

      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.PC).SetValue(expectedAddress);

      var memory = new Memory();
      memory.SetValue(expectedAddress, expectedValue);

      var fetcher = new Fetcher(model, memory);
      Assert.That(fetcher.Fetch(), Is.EqualTo(expectedValue));
    }

    [Test]
    public void IncrementsThePCOneByte()
    {
      const ushort initialAddress = 0xFF10;

      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.PC).SetValue(initialAddress);

      var memory = new Memory();

      var fetcher = new Fetcher(model, memory);
      fetcher.Fetch();
      Assert.That(model.GetRegister(RegisterName.PC).GetValue(),
          Is.EqualTo(initialAddress + 1));
    }
  }
}
