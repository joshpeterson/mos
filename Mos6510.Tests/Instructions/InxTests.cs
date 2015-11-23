using NUnit.Framework;
using Mos6510;
using Mos6510.Instructions;

namespace Mos6510.Tests
{
  [TestFixture]
  public class InxTests
  {
    [Test]
    public void IncrementsTheValueInRegisterX()
    {
      const int initialValue = 42;

      var model = new ProgrammingModel();
      model.GetRegister(RegisterName.X).SetValue(initialValue);
      var instruction = new Inx();
      instruction.Execute(model);
      Assert.That(model.GetRegister(RegisterName.X).GetValue(),
                  Is.EqualTo(initialValue + 1));
    }
  }
}