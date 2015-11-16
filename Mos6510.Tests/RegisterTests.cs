using Mos6510;
using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  class RegisterTests
  {
    [Test]
    public void CanCreateRegisterOfaGivenSize()
    {
      var register = new Register(8);
      Assert.That(register.Length, Is.EqualTo(8));
    }

    [Test]
    public void HasAnInitialValueOfZero()
    {
      var register = new Register(8);
      Assert.That(register.Value, Is.EqualTo(0));
    }

    [Test]
    public void CanSetTheValueOfARegister()
    {
      var register = new Register(8);
      const int expectedValue = 42;
      register.Value = expectedValue;
      Assert.That(register.Value, Is.EqualTo(expectedValue));
    }
  }
}
