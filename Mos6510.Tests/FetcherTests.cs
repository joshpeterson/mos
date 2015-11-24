using NUnit.Framework;
using Mos6510;

namespace Mos6510.Tests
{
  [TestFixture]
  public class FetcherTests
  {
    [Test]
    public void FetchesTheNextInstructionsFromTheProgramCounter()
    {
      const ushort expectedAddress = 0xFF10;
      const byte expectedValue = 0x2A;

      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.PCH).
        SetValue((expectedAddress & 0xFF00) >> 8);
      model.GetRegister(RegisterName.PCL).
        SetValue(expectedAddress & 0x00FF);

      var memory = new Memory();
      memory.SetValue(expectedAddress, expectedValue);

      var fetcher = new Fetcher(model, memory);
      Assert.That(fetcher.Fetch(), Is.EqualTo(expectedValue));
    }
  }
}
