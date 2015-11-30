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
  }
}
