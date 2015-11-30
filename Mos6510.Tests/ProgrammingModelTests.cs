using Mos6510;
using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class ProgrammingModelTests
  {
    [TestCase(RegisterName.A)]
    [TestCase(RegisterName.Y)]
    [TestCase(RegisterName.X)]
    [TestCase(RegisterName.PC)]
    [TestCase(RegisterName.S)]
    [TestCase(RegisterName.P)]
    public void CanSetTheValueInRegister(RegisterName name)
    {
      const int expectedValue = 42;

      var model = new ProgrammingModel();

      var registerBeforeSet = model.GetRegister(name);
      registerBeforeSet.SetValue(expectedValue);

      var registerAfterSet = model.GetRegister(name);
      Assert.That(registerAfterSet.GetValue(), Is.EqualTo(expectedValue));
    }
  }
}
