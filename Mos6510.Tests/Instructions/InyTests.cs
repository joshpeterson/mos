using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class InyTests
  {
    [Test]
    public void IncrementsTheValueInRegisterY()
    {
      const int initialValue = 42;

      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.Y).SetValue(initialValue);
      var instruction = new Iny();
      instruction.Execute(model);
      Assert.That(model.GetRegister(RegisterName.Y).GetValue(),
                  Is.EqualTo(initialValue + 1));
    }
  }
}
