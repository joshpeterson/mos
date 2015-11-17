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
      Assert.That(register.GetValue(), Is.EqualTo(0));
    }

    [Test]
    public void CanSetTheValueOfARegister()
    {
      var register = new Register(8);
      const int expectedValue = 42;
      register.SetValue(expectedValue);
      Assert.That(register.GetValue(), Is.EqualTo(expectedValue));
    }

    [Test]
    public void AValueWhichIsTooLargeSetsOnlyTheLowBitsOfTheRegister()
    {
      var register = new Register(8);
      register.SetValue(258);
      Assert.That(register.GetValue(), Is.EqualTo(2));
    }

    [Test]
    public void ANegativeValueIsThePostiveRepresentationOfTheBits()
    {
      var register = new Register(8);
      register.SetValue(-100);
      Assert.That(register.GetValue(), Is.EqualTo(156));
    }
  }
}
